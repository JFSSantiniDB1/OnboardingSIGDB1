using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;

namespace OnboardingSIGDB1.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly SIGDB1DbContext _contexto;
        private readonly DbSet<T> _set;
        
        public GenericRepository(SIGDB1DbContext contexto)
        {
            _contexto = contexto;
            _set = _contexto.Set<T>();
        }
        
        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }

        public T Get(Expression<Func<T, bool>> funcFilter)
        {
            return _set.FirstOrDefault(funcFilter.Expand());
        }

        public void Remove(T entity)
        {
            _contexto.Remove(entity);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }
    }
}