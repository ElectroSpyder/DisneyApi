namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.LogicRepositories.Repository;
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class PeliculaSerieRepository: IPeliculaSerieRepository
    {
        private readonly DisneyDBContext _context;
        public PeliculaSerieRepository( DisneyDBContext context)
        {
            _context = context;
        }

        public async Task<PeliculaSerie> Add(PeliculaSerie entity)
        {          
           
            _context.Set<PeliculaSerie>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PeliculaSerie> Delete(int id)
        {
            var entity = await _context.Set<PeliculaSerie>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<PeliculaSerie>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PeliculaSerie> Get(int id)
        {
            return await _context.Set<PeliculaSerie>().FindAsync(id);
        }

        public async Task<List<PeliculaSerie>> GetAll()
        {
            return await _context.Set<PeliculaSerie>().ToListAsync();
        }

        public async Task<IQueryable<PeliculaSerie>> GetByFunc(Expression<Func<PeliculaSerie, bool>> filter, string order = null)
        {
            if (filter == null) return null;
            if (order == null) order = "ASC";
            if (order.ToUpper().Trim() == "DESC")
            {
                var result = await _context.Set<PeliculaSerie>().Where(filter).OrderByDescending(filter).ToListAsync();
                return result.AsQueryable();
            }

            return _context.Set<PeliculaSerie>().Where(filter).OrderBy(filter).AsQueryable();
        }

        public async Task<PeliculaSerie> Update(PeliculaSerie entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
