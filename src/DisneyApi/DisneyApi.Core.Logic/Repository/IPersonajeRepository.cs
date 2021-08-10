namespace DisneyApi.Core.LogicRepositories.Repository
{
    using DisneyApi.Core.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IPersonajeRepository
    {
        Task<List<Personaje>> GetAll();
        Task<Personaje> Get(int id);
        Task<Personaje> Add(Personaje entity);
        Task<Personaje> Update(Personaje entity);
        Task<Personaje> Delete(int id);
        Task<IQueryable<Personaje>> GetByFunc(Expression<Func<Personaje, bool>> filter, string order = null);
    }
}
