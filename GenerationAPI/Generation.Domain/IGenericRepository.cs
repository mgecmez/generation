using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Domain
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task Save(T entity);
        Task<T> Get(Guid id);
        Task Update(T entity);
        Task Delete(Guid id);
        IQueryable<T> All();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
