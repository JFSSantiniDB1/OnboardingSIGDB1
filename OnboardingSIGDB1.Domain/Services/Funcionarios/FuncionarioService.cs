using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services.Funcionarios
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly INotificationContext _notification;
        private readonly IFuncionarioValidatorService _validator;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository, 
            INotificationContext notification, IFuncionarioValidatorService validator)
        {
            _funcionarioRepository = funcionarioRepository;
            _notification = notification;
            _validator = validator;
        }
        
        public IList<FuncionarioDto> GetAll(FiltroFuncionarioDto filtro)
        {
            Expression<Func<Funcionario, bool>> exp = x => x.Id != 0;

            if(filtro.IdEmpresa.HasValue)
                exp = CombineExpressions<Funcionario>.And(exp, x => x.IdEmpresa == filtro.IdEmpresa);
            if(!string.IsNullOrEmpty(filtro.Nome))
                exp = CombineExpressions<Funcionario>.And(exp, x => x.Nome.Contains(filtro.Nome));
            if (!string.IsNullOrEmpty(filtro.Cpf))
            {
                var cpfSemMascara = Convertions.GetCpfSemMascara(filtro.Cpf);
                exp = CombineExpressions<Funcionario>.And(exp, x => x.Cpf.Equals(cpfSemMascara));
            }
            if(filtro.DataContratacaoInicio.HasValue)
                exp = CombineExpressions<Funcionario>.And(exp, x => x.DataContratacao >= filtro.DataContratacaoInicio.Value);
            if(filtro.DataContratacaoFim.HasValue)
                exp = CombineExpressions<Funcionario>.And(exp, x => x.DataContratacao <= filtro.DataContratacaoFim.Value);
            
            return _funcionarioRepository.GetAll(exp)
                .Select(x => BaseMapper.Mapper.Map<FuncionarioDto>(x)).ToList();
        }
        
        public FuncionarioDto Get(int id)
        {
            var funcionario = _funcionarioRepository.Get(x => x.Id == id);
            if (funcionario == null)
            {
                _notification.AddNotification("No Content", "Funcionário não encontrado.");
                return null;
            }
            var funcionarioDto = BaseMapper.Mapper.Map<FuncionarioDto>(funcionario);
            return funcionarioDto;
        }
        
        public int Add(FuncionarioDto funcionarioDto)
        {
            var funcionario = BaseMapper.Mapper.Map<Funcionario>(funcionarioDto);
            
            funcionario.Validate(funcionario, _validator);
            if(funcionario.Invalid)
                _notification.AddNotifications(funcionario.ValidationResult);
            else
            {
                _funcionarioRepository.Add(funcionario);
                _funcionarioRepository.Commit();
                return funcionario.Id;
            }
            return 0;
        }
        
        public int Update(FuncionarioDto funcionarioDto)
        {
            var funcionario = _funcionarioRepository.Get(x => x.Id == funcionarioDto.Id);
            if (funcionario == null)
            {
                _notification.AddNotification("No Content", "Funcionário não encontrado.");
                return 0;
            }

            funcionario.SetCpf(Convertions.GetCpfSemMascara(funcionarioDto.Cpf));
            funcionario.SetNome(funcionarioDto.Nome);
            funcionario.SetDataContratacao(Convertions.GetDateTime(funcionarioDto.DataContratacao));
            if(funcionario.IdEmpresa != funcionarioDto.IdEmpresa)
                _notification.AddNotification("Unprocessable Entity", "Não é permitido alterar a empresa do funcionário.");
        
            funcionario.Validate(funcionario, _validator);
            if(funcionario.Invalid)
                _notification.AddNotifications(funcionario.ValidationResult);
            else
            {
                _funcionarioRepository.Update(funcionario);
                _funcionarioRepository.Commit();
                return funcionario.Id;
            }

            return 0;
        }

        public int Delete(int id)
        {
            var funcionario = _funcionarioRepository.Get(x => x.Id == id);
            var idReturn = 0;
            if (funcionario == null)
                _notification.AddNotification("No Content", "Funcionário não encontrado.");
            else
            {
                try
                {
                    idReturn = funcionario.Id;
                    _funcionarioRepository.Remove(funcionario);
                    _funcionarioRepository.Commit();
                }
                catch (Exception ex) when (ex is DbUpdateException ||
                                           ex is InvalidOperationException)
                {
                    _notification.AddNotification("Unprocessable Entity", "Não é permitirdo excluir o registro pois o mesmo possui vínculos.");
                    idReturn = 0;
                }
            }
            return idReturn;
        }
        
        public IList<Notification> GetNotifications()
        {
            return _notification.Notifications.ToList();
        }
    }
}