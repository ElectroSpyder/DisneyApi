namespace DisneyApi.Core.Repositories.UnitOfWork
{
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Repositories.GenericRepository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<Genero> _generoRepository;
        private IGenericRepository<PeliculaSerie> _peliculaSerieRepository;
        private IGenericRepository<Personaje> _personajeRepository;
        private readonly DisneyDBContext _disneyDBContext;

        private bool disposed;

        public UnitOfWork(DisneyDBContext disneyDBContext)
        {
            _disneyDBContext = disneyDBContext;
        }

        public IGenericRepository<Genero> GeneroRepository
        {
            get
            {
                return _generoRepository ??= new GenericRepository<Genero>(_disneyDBContext);
            }
        }
        public IGenericRepository<PeliculaSerie> PeliculaSerieRepository { get
            {
                return _peliculaSerieRepository ??= new GenericRepository<PeliculaSerie>(_disneyDBContext);
            }
        }
        public IGenericRepository<Personaje> PersonajeRepository
        {
            get
            {
                return _personajeRepository ??= new GenericRepository<Personaje>(_disneyDBContext);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _disneyDBContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _disneyDBContext.Dispose();
                }
            }
            disposed = true;
        }
    }
}
