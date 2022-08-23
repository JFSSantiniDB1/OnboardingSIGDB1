using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Cargos;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Interfaces.Services
{
    public interface ICargoService
    {
        IList<CargoDto> GetAll(FiltroCargoDto filtro);
        CargoDto Get(int id);
        int Add(CargoDto cargoDto);
        int Update(CargoDto cargoDto);
        int Delete(int id);
        List<Notification> GetNotifications();
    }
}