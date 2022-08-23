using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OnboardingSIGDB1.Domain.Entities;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface ICargoRepository : IGenericRepository<Cargo>
    {
        bool GetDescricaoAlreadyExists(int entityId, string entityDescricao);
    }
}