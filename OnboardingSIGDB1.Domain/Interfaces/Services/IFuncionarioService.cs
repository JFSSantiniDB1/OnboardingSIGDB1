using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;

namespace OnboardingSIGDB1.Domain.Interfaces.Services
{
    public interface IFuncionarioService
    {
        IList<FuncionarioDto> GetAll(FiltroFuncionarioDto filtro);
        FuncionarioDto Get(int id);
        int Add(FuncionarioDto funcionarioDto);
        int Update(FuncionarioDto funcionarioDto);
        int Delete(int id);
        IList<Notification> GetNotifications();
    }
}