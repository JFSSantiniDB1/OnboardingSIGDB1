using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface IFuncionarioXCargoRepository : IGenericRepository<FuncionarioXCargo>
    {
        FuncionarioXCargo Get(Expression<Func<FuncionarioXCargo, bool>> exp);
        IList<FuncionarioXCargo> GetAll(Expression<Func<FuncionarioXCargo, bool>> exp);
        bool GetHaveLinkWithCompany(int idFuncionario);
        bool GetLinkAlreadyExists(int entityId, int entityIdFuncionario, int entityIdCargo);
        bool GetCargoExists(int idCargo);
        bool GetFuncionarioExists(int idFuncionario);
    }
}