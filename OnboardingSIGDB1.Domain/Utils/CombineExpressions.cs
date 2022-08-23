using System;
using System.Linq.Expressions;
using LinqKit;

namespace OnboardingSIGDB1.Domain.Utils
{
    public static class CombineExpressions<T> where T:class
    {
        public static Expression<Func<T, bool>> And(Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            return x => exp1.Invoke(x) && exp2.Invoke(x);
        }
    }
}