using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;

namespace OnboardingSIGDB1.Data.Repositories
{
    public class CargoRepository : GenericRepository<Cargo>, ICargoRepository
    {
        private readonly SIGDB1DbContext _contexto;

        public CargoRepository(SIGDB1DbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public bool GetDescricaoAlreadyExists(int entityId, string entityDescricao)
        {
            return _contexto.Cargo.Any(x => x.Id != entityId && x.Descricao == entityDescricao);
        }
    }
}