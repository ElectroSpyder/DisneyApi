namespace DisneyApi.Core.Repositories.GenericRepository
{
    using DisneyApi.Core.Models.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DisneyDBContext _dBContext;
        //private readonly DbSet<T> _dbSet;

        public GenericRepository(DisneyDBContext dBContext)
        {
            _dBContext = dBContext;
            //_dbSet = _dBContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await _dBContext.Set<T>().AddAsync(entity);
           // await _dBContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _dBContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

             _dBContext.Set<T>().Remove(entity);
            //await _dBContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await _dBContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dBContext.Set<T>().ToListAsync();
        }

        public async Task<IQueryable<T>> GetByFunc(Expression<Func<T, bool>> filter, string order = null)
        {
            if (filter == null) return null;
            if (order == null) order = "ASC";
            if (order.ToUpper().Trim() == "DESC")
            {
                var result = await _dBContext.Set<T>().Where(filter).OrderByDescending(filter).ToListAsync();
                return result.AsQueryable();
            }

            return _dBContext.Set<T>().Where(filter).OrderBy(filter).AsQueryable();
        }

        public async Task<T> Update(T entity, int id)
        {
            var exist = await _dBContext.Set<T>().FindAsync(id);
            if (exist != null)
            {
                _dBContext.Entry(entity).State = EntityState.Modified;
            }            
            
           // await _dBContext.SaveChangesAsync();
            return  entity;
        }
    }
}
