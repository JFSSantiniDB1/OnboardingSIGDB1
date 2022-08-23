using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Empresas;

namespace OnboardingSIGDB1.Domain.Interfaces.Services
{
    public interface IEmpresaService
    {
        IList<EmpresaDto> GetAll(FiltroEmpresaDto filtro);
        EmpresaDto Get(int id);
        int Add(EmpresaDto empresaDto);
        int Update(EmpresaDto empresaDto);
        int Delete(int id);
        IList<Notification> GetNotifications();
    }
}