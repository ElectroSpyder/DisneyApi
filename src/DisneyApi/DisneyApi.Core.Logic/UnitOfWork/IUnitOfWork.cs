namespace DisneyApi.Core.Repositories.UnitOfWork
{
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Repositories.GenericRepository;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Genero> GeneroRepository { get;  }
        IGenericRepository<PeliculaSerie> PeliculaSerieRepository { get;  }
        IGenericRepository<Personaje> PersonajeRepository { get;  }

        Task<bool> SaveAsync();
    }
}
