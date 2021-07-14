namespace DisneyApi.Core.Models.Repository
{
    using DisneyApi.Core.Models.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T> where T : class
    {
        protected DisneyDBContext DisneyDBContext { get; set; }

        public Repository(DisneyDBContext disneyDBContext)
        {
            DisneyDBContext = disneyDBContext;
        }

        public void Create(T entity)
        {
            this.DisneyDBContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.DisneyDBContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.DisneyDBContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DisneyDBContext.Set<T>()
           .Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            this.DisneyDBContext.Set<T>().Update(entity);
        }
    }
}
