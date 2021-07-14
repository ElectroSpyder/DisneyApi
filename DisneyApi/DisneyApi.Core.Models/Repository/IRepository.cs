﻿namespace DisneyApi.Core.Models.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
