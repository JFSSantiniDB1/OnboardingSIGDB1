using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;

namespace OnboardingSIGDB1.Data.Repositories
{
    public class EmpresaRepository : GenericRepository<Empresa>, IEmpresaRepository
    {
        private readonly SIGDB1DbContext _contexto;

        public EmpresaRepository(SIGDB1DbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        
        public Empresa Get(Expression<Func<Empresa, bool>> funcFilter)
        {
            return _contexto.Empresa
                .Include(x => x.Funcionarios)
                .ThenInclude(x => x.Cargos)
                .FirstOrDefault(funcFilter.Expand());
        }

        public Empresa GetEntityOnly(Expression<Func<Empresa, bool>> funcFilter)
        {
            return _contexto.Empresa
                .FirstOrDefault(funcFilter.Expand());
        }

        public bool GetCnpjAlreadyExists(int id, string cnpj)
        {
            return _contexto.Empresa.Any(x => x.Id != id && x.Cnpj == cnpj);
        }
    }
}