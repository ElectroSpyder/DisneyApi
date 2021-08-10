namespace DisneyApi.Core.LogicRepositories.Repository
{
    using DisneyApi.Core.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IPeliculaSerieRepository
    {
        Task<List<PeliculaSerie>> GetAll();
        Task<PeliculaSerie> Get(int id);
        Task<PeliculaSerie> Add(PeliculaSerie entity);
        Task<PeliculaSerie> Update(PeliculaSerie entity);
        Task<PeliculaSerie> Delete(int id);
        Task<IQueryable<PeliculaSerie>> GetByFunc(Expression<Func<PeliculaSerie, bool>> filter, string order = null);
    }
}
