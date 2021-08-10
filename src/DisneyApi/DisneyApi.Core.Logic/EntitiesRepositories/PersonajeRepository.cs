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

    public class PersonajeRepository : IPersonajeRepository
    {
        private readonly DisneyDBContext _context;
        public PersonajeRepository(DisneyDBContext context)
        {
            _context = context;
        }

        public async Task<Personaje> Add(Personaje entity)
        {
            _context.Set<Personaje>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Personaje> Delete(int id)
        {
            var entity = await _context.Set<Personaje>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<Personaje>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Personaje> Get(int id)
        {
            return await _context.Set<Personaje>().FindAsync(id);
        }

        public async Task<List<Personaje>> GetAll()
        {
            return await _context.Set<Personaje>().ToListAsync();
        }

        public async Task<IQueryable<Personaje>> GetByFunc(Expression<Func<Personaje, bool>> filter, string order = null)
        {
            if (filter == null) return null;
            if (order == null) order = "ASC";
            if (order.ToUpper().Trim() == "DESC")
            {
                var result = await _context.Set<Personaje>().Where(filter).OrderByDescending(filter).ToListAsync();
                return result.AsQueryable();
            }

            return _context.Set<Personaje>().Where(filter).OrderBy(filter).AsQueryable();
        }

        public async Task<Personaje> Update(Personaje entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
