using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ingress.Data.Interfaces
{
    public interface IEntityRepository<TEntity, in TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void CancelChanges(TEntity entity);
        Task SaveChanges();
    }
}