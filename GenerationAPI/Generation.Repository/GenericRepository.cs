using Generation.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Generation.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly GenerationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected GenericRepository(GenerationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> All()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task Delete(Guid id)
        {
            var entity = await Get(id);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }

        public async Task<T> Get(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(T entity)
        {
            entity.Id = Guid.NewGuid();
            await _dbSet.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
