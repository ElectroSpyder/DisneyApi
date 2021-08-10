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

    public class GeneroRepository: IGeneroRepository
    {
        private readonly DisneyDBContext _context;
        public GeneroRepository(DisneyDBContext context)
        {
            _context = context;
        }

        public async Task<Genero> Add(Genero entity)
        {
            _context.Set<Genero>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Genero> Delete(int id)
        {
            var entity = await _context.Set<Genero>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<Genero>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Genero> Get(int id)
        {
            return await _context.Set<Genero>().FindAsync(id);
        }

        public async Task<List<Genero>> GetAll()
        {
            return await _context.Set<Genero>().ToListAsync();
        }

        public async Task<IQueryable<Genero>> GetByFunc(Expression<Func<Genero, bool>> filter, string order = null)
        {
            if (filter == null) return null;
            if (order == null) order = "ASC";
            if (order.ToUpper().Trim() == "DESC")
            {
                var result = await _context.Set<Genero>().Where(filter).OrderByDescending(filter).ToListAsync();
                return result.AsQueryable();
            }

            return _context.Set<Genero>().Where(filter).OrderBy(filter).AsQueryable();
        }

        public async Task<Genero> Update(Genero entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
