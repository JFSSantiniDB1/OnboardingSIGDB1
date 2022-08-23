using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface IEmpresaRepository : IGenericRepository<Empresa>
    {
        Empresa Get(Expression<Func<Empresa, bool>> funcFilter);
        Empresa GetEntityOnly(Expression<Func<Empresa, bool>> funcFilter);
        IList<Empresa> GetEmpresas(Expression<Func<Empresa, bool>> exp);
        bool GetCnpjAlreadyExists(int id, string cnpj);
    }
}