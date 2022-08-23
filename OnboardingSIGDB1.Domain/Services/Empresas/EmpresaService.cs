using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Empresas;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services.Empresas
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly INotificationContext _notification;
        private readonly IEmpresaValidatorService _validator;

        public EmpresaService(IEmpresaRepository empresaRepository, 
            INotificationContext notification, IEmpresaValidatorService validator
            )
        {
            _empresaRepository = empresaRepository;
            _notification = notification;
            _validator = validator;
        }
        
        public IList<EmpresaDto> GetAll(FiltroEmpresaDto filtro)
        {
            Expression<Func<Empresa, bool>> exp = x => x.Id != 0;

            if (!string.IsNullOrEmpty(filtro.Nome))
                exp = CombineExpressions<Empresa>.And(exp, x => x.Nome.Contains(filtro.Nome));
            if (!string.IsNullOrEmpty(filtro.Cnpj))
            {
                var cnpjSemMascara = Convertions.GetCnpjSemMascara(filtro.Cnpj);
                exp = CombineExpressions<Empresa>.And(exp, x => x.Cnpj == cnpjSemMascara);
            }
            if(filtro.DataFundacaoInicio.HasValue)
                exp = CombineExpressions<Empresa>.And(exp, x => x.DataFundacao >= filtro.DataFundacaoInicio.Value);
            if(filtro.DataFundacaoFim.HasValue)
                exp = CombineExpressions<Empresa>.And(exp, x => x.DataFundacao <= filtro.DataFundacaoFim.Value);

            return _empresaRepository.GetEmpresas(exp).Select(x => BaseMapper.Mapper.Map<EmpresaDto>(x)).ToList();
        }
        
        public EmpresaDto Get(int id)
        {
            var empresa = _empresaRepository.Get(x => x.Id == id);
            if (empresa == null)
            {
                _notification.AddNotification("No Content", "Empresa não encontrada.");
                return null;
            }
            var empresaDto = BaseMapper.Mapper.Map<EmpresaDto>(empresa);

            return empresaDto;
        }

        public int Add(EmpresaDto empresaDto)
        {
            var empresa = BaseMapper.Mapper.Map<Empresa>(empresaDto);
            empresa.Validate(empresa, _validator);
            if (empresa.Invalid)
                _notification.AddNotifications(empresa.ValidationResult);
            else 
            {
                _empresaRepository.Add(empresa);
                _empresaRepository.Commit();

                return empresa.Id;
            }
            return 0;
        }

        public int Update(EmpresaDto empresaDto)
        {
            var empresa = _empresaRepository.GetEntityOnly(x => x.Id == empresaDto.Id);
            if (empresa == null)
            {
                _notification.AddNotification("No Content","Empresa não encontrada.");
                return 0;
            }
            empresa.SetCnPj(Convertions.GetCnpjSemMascara(empresaDto.Cnpj));
            empresa.SetNome(empresa.Nome);
            empresa.SetDataFundacao(empresa.DataFundacao);
            
            empresa.Validate(empresa, _validator);

            if (empresa.Invalid)
                _notification.AddNotifications(empresa.ValidationResult);
            else
            {
                _empresaRepository.Update(empresa);
                _empresaRepository.Commit();
                return empresa.Id;
            }

            return 0;
        }

        public int Delete(int id)
        {
            var empresa = _empresaRepository.GetEntityOnly(x => x.Id == id);
            var idReturn = 0;
            if (empresa == null)
                _notification.AddNotification("No Content", "Empresa não encontrada.");
            else
            {
                try
                {
                    idReturn = empresa.Id;
                    _empresaRepository.Remove(empresa);
                    _empresaRepository.Commit();
                }
                catch (DbUpdateException e)
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