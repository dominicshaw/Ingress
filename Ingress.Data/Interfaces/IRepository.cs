using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ingress.Data.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}