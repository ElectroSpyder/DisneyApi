namespace DisneyApi.Core.Repositories.GenericRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IGenericRepository<T> where T: class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity, int id);
        Task<T> Delete(int id);
        Task<IQueryable<T>> GetByFunc(Expression<Func<T, bool>> filter, string order = null);
    }
}
