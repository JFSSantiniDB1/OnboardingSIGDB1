

using System;
using System.Linq.Expressions;

namespace OnboardingSIGDB1.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        void Add(T entity);
        void Update(T entity);
        T Get(Expression<Func<T, bool>> funcFilter);
        void Remove(T entity);
        void Commit();
    }
}