using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicShop.Core.Repositories
{
    public interface IRepository<Entity> where Entity: class
    {
        ValueTask<Entity> GetByIdAsync(int id);
        Task<IEnumerable<Entity>> GetAllAsync();
        IEnumerable<Entity> Find(Expression<Func<Entity, bool>> predicate);
        Task AddAsync(Entity entity);
        Task AddRangeAsync(IEnumerable<Entity> entities);
        void Remove(Entity entity);
        void RemoveRange(IEnumerable<Entity> entities);
        Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> predicate);
    }
}
