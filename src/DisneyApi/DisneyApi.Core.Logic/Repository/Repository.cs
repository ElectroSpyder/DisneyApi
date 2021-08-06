namespace DisneyApi.Core.Logic.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<T, TContext> : IRepository<T> where T : class
                                                          where TContext : DbContext
    {
        private readonly TContext context;

        public Repository(TContext dbContext)
        {
            context = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }      

        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Return null if not found
        /// </summary>
        /// <param name="filter">exampl filter = (x=> x.attribut == model.attribut), orderBy  DESC or ASC</param>
        /// <returns>List<T></returns>
        public IQueryable<T> GetByFunc(Expression<Func<T, bool>> filter, string order=null)
        {
            if (filter == null) return null;
            if (order == null) order = "ASC";
            if (order.ToUpper().Trim() == "DESC") return context.Set<T>().Where(filter).OrderByDescending(filter).AsQueryable();
            
            return  context.Set<T>().Where(filter).OrderBy(filter).AsQueryable();
        }       
    }
}
