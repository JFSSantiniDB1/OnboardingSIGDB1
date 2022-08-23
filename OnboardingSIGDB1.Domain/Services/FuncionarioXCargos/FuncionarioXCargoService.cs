using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services.FuncionarioXCargos
{
    public class FuncionarioXCargoService : IFuncionarioXCargoService
    {
        private readonly IFuncionarioXCargoRepository _funcionarioXCargoRepository;
        private readonly INotificationContext _notification;
        private readonly IFuncionarioXCargoValidatorService _validator;

        public FuncionarioXCargoService(IFuncionarioXCargoRepository funcionarioXCargoRepository, 
            INotificationContext notification, IFuncionarioXCargoValidatorService validator)
        {
            _funcionarioXCargoRepository = funcionarioXCargoRepository;
            _notification = notification;
            _validator = validator;
        }
        public IList<FuncionarioXCargoDto> GetAll(FiltroFuncionarioXCargoDto filtro)
        {
            Expression<Func<FuncionarioXCargo, bool>> exp = x => x.Id != 0;

            if(filtro.IdFuncionario.HasValue)
                exp = CombineExpressions<FuncionarioXCargo>.And(exp, x => x.IdFuncionario == filtro.IdFuncionario);
            if(filtro.IdCargo.HasValue)
                exp = CombineExpressions<FuncionarioXCargo>.And(exp, x => x.IdCargo == filtro.IdCargo);

            return _funcionarioXCargoRepository.GetListFuncionarioXCargo(exp)
                .Select(x => BaseMapper.Mapper.Map<FuncionarioXCargoDto>(x)).ToList();
        }
        
        public FuncionarioXCargoDto Get(int id)
        {
            var funcionario = _funcionarioXCargoRepository.Get(x => x.Id == id);
            if (funcionario == null)
            {
                _notification.AddNotification("No Content", "Cargo de funcionário não encontrado.");
                return null;
            }
            var funcionarioDto = BaseMapper.Mapper.Map<FuncionarioXCargoDto>(funcionario);
            return funcionarioDto;
        }

        public int Add(FuncionarioXCargoDto funcionarioXCargoDto)
        {
            var funcionarioXCargo = BaseMapper.Mapper.Map<FuncionarioXCargo>(funcionarioXCargoDto);
            funcionarioXCargo.Validate(funcionarioXCargo, _validator);
            if(funcionarioXCargo.Invalid)
                _notification.AddNotifications(funcionarioXCargo.ValidationResult);
            else
            {
                _funcionarioXCargoRepository.Add(funcionarioXCargo);
                _funcionarioXCargoRepository.Commit();
                return funcionarioXCargo.Id;
            }
            return 0;
        }

        public int Delete(int id)
        {
            var funcionarioXCargo = _funcionarioXCargoRepository.Get(x => x.Id == id);
            var idReturn = 0;
            
            if (funcionarioXCargo == null)
                _notification.AddNotification("No Content", "Funcionário não encontrado.");
            else
            {
                try
                {
                    idReturn = funcionarioXCargo.Id;
                    _funcionarioXCargoRepository.Remove(funcionarioXCargo);
                    _funcionarioXCargoRepository.Commit();
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