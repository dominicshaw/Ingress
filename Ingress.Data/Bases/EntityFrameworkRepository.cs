using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Ingress.Data.Bases
{
    public class EntityFrameworkRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        private readonly DbContext _context;

        protected EntityFrameworkRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<TEntity>> GetAll()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public Task<TEntity> GetById(TKey id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public void Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Attach(entity);
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Reload(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.Entry(entity).ReloadAsync();
        }

        public void CancelChanges(TEntity entity)
        {
            _context.Entry(entity).CurrentValues.SetValues(_context.Entry(entity).OriginalValues);
            _context.Entry(entity).State = EntityState.Unchanged;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}