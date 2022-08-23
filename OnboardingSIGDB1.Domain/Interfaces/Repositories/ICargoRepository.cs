using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface ICargoRepository : IGenericRepository<Cargo>
    {
        IList<Cargo> GetCargos(Expression<Func<Cargo, bool>> exp);
        bool GetDescricaoAlreadyExists(int entityId, string entityDescricao);
    }
}