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
    public class FuncionarioRepository : GenericRepository<Funcionario>, IFuncionarioRepository
    {
        private readonly SIGDB1DbContext _contexto;

        public FuncionarioRepository(SIGDB1DbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        
        public Funcionario Get(Expression<Func<Funcionario, bool>> funcFilter)
        {
            return _contexto.Funcionario
                .Include(x => x.Empresa)
                .Include(x => x.Cargos)
                .ThenInclude(x => x.Cargo)
                .FirstOrDefault(funcFilter.Expand());
        }

        public bool GetCpfAlreadyExists(int id, string cpf)
        {
            return _contexto.Funcionario.Any(x => x.Id != id && x.Cpf == cpf);
        }
        
    }
}