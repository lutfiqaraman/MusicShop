using Microsoft.EntityFrameworkCore;
using MusicShop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicShop.Data.SQL.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public async Task AddAsync(Entity entity)
        {
            await Context.Set<Entity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Entity> entities)
        {
            await Context.Set<Entity>().AddRangeAsync(entities);
        }

        public IEnumerable<Entity> Find(Expression<Func<Entity, bool>> predicate)
        {
            return Context.Set<Entity>().Where(predicate);
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await Context.Set<Entity>().ToListAsync();
        }

        public ValueTask<Entity> GetByIdAsync(int id)
        {
            return Context.Set<Entity>().FindAsync(id);
        }

        public void Remove(Entity entity)
        {
            Context.Set<Entity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<Entity> entities)
        {
            Context.Set<Entity>().RemoveRange(entities);
        }

        public Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> predicate)
        {
            return Context.Set<Entity>().SingleOrDefaultAsync(predicate);
        }
    }
}
