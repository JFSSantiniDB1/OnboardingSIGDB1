using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Cargos;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services.Cargos
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly INotificationContext _notification;
        private readonly ICargoValidatorService _validator;

        public CargoService(ICargoRepository cargoRepository, 
            INotificationContext notification, ICargoValidatorService validator)
        {
            _cargoRepository = cargoRepository;
            _notification = notification;
            _validator = validator;
        }
        
        public IList<CargoDto> GetAll(FiltroCargoDto filtro)
        {
            Expression<Func<Cargo, bool>> exp = x => x.Id != 0;
            
            if(!string.IsNullOrEmpty(filtro.Descricao))
                exp = CombineExpressions<Cargo>.And(exp, x => x.Descricao.Contains(filtro.Descricao));

            return _cargoRepository.GetAll(exp).Select(x => BaseMapper.Mapper.Map<CargoDto>(x)).ToList();
        }
        
        public CargoDto Get(int id)
        {
            var cargo = _cargoRepository.Get(x => x.Id == id);
            if (cargo == null)
            {
                _notification.AddNotification("No Content", "Cargo não encontrado.");
                return null;
            }
            return BaseMapper.Mapper.Map<CargoDto>(cargo);
        }
        
        public int Add(CargoDto cargoDto)
        {
            var cargo = BaseMapper.Mapper.Map<Cargo>(cargoDto);
            cargo.Validate(cargo, _validator);
            
            if(cargo.Invalid)
                _notification.AddNotifications(cargo.ValidationResult);
            else
            {
                _cargoRepository.Add(cargo);
                _cargoRepository.Commit();
                return cargo.Id;
            }
            return 0;
        }

        public int Update(CargoDto cargoDto)
        {
            var cargo = _cargoRepository.Get(x => x.Id == cargoDto.Id);
            if (cargo == null)
            {
                _notification.AddNotification("No Content","Cargo não encontrado.");
                return 0;
            }
            cargo.SetDescricao(cargoDto.Descricao);
            cargo.Validate(cargo, _validator);
            if(cargo.Invalid)
                _notification.AddNotifications(cargo.ValidationResult);
            else
            {
                _cargoRepository.Update(cargo);
                _cargoRepository.Commit();
                return cargo.Id;
            }
            return 0;
        }
        
        public int Delete(int id)
        {
            var cargo = _cargoRepository.Get(x => x.Id == id);
            var idReturn = 0;
            if (cargo == null)
                _notification.AddNotification("No Content", "Cargo não encontrado.");
            else
            {
                try
                {
                    idReturn = cargo.Id;
                    _cargoRepository.Remove(cargo);
                    _cargoRepository.Commit();
                }
                catch (DbUpdateException e)
                {
                    _notification.AddNotification("Unprocessable Entity", "Não é permitirdo excluir o registro pois o mesmo possui vínculos.");
                    idReturn = 0;
                }
            }
            return idReturn;
        }

        public List<Notification> GetNotifications()
        {
            return _notification.Notifications.ToList();
        }
    }
}