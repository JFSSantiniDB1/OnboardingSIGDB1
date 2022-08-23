using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos;

namespace OnboardingSIGDB1.Domain.Interfaces.Services
{
    public interface IFuncionarioXCargoService
    {
        IList<FuncionarioXCargoDto> GetAll(FiltroFuncionarioXCargoDto filtro);
        FuncionarioXCargoDto Get(int id);
        int Add(FuncionarioXCargoDto funcionarioXCargoDto);
        int Delete(int id);
        IList<Notification> GetNotifications();
    }
}