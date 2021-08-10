namespace DisneyApi.Core.LogicRepositories.Repository
{
    using DisneyApi.Core.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGeneroRepository
    {
        Task<List<Genero>> GetAll();
        Task<Genero> Get(int id);
        Task<Genero> Add(Genero entity);
        Task<Genero> Update(Genero entity);
        Task<Genero> Delete(int id);
        Task<IQueryable<Genero>> GetByFunc(Expression<Func<Genero, bool>> filter, string order = null);
    }
}
