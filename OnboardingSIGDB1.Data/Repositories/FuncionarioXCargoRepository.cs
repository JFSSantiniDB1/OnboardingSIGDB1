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
    public class FuncionarioXCargoRepository : GenericRepository<FuncionarioXCargo>, IFuncionarioXCargoRepository
    {
        private readonly SIGDB1DbContext _contexto;

        public FuncionarioXCargoRepository(SIGDB1DbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public FuncionarioXCargo Get(Expression<Func<FuncionarioXCargo, bool>> exp)
        {
            return _contexto.FuncionarioXCargo
                .Include(x => x.Cargo)
                .Include(x => x.Funcionario)
                .FirstOrDefault(exp.Expand());
        }
        
        public IList<FuncionarioXCargo> GetListFuncionarioXCargo(Expression<Func<FuncionarioXCargo, bool>> exp)
        {
            return _contexto.FuncionarioXCargo
                .Include(x => x.Cargo)
                .Include(x => x.Funcionario)
                .Where(exp.Expand())
                .ToList();
        }

        public bool GetCargoExists(int idCargo)
        {
            return _contexto.Cargo.Any(x => x.Id == idCargo);
        }

        public bool GetFuncionarioExists(int idFuncionario)
        {
            return _contexto.Funcionario.Any(x => x.Id == idFuncionario);
        }

        public bool GetHaveLinkWithCompany(int idFuncionario)
        {
            return _contexto.Funcionario.Any(x => x.Id == idFuncionario && x.IdEmpresa.HasValue);
        }

        public bool GetLinkAlreadyExists(int id, int idFuncionario, int idCargo)
        {
            return _contexto.FuncionarioXCargo.Any(x =>
                    x.Id != id && x.IdFuncionario == idFuncionario && x.IdCargo == idCargo);
        }
    }
}
