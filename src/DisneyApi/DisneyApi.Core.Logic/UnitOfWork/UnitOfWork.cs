namespace DisneyApi.Core.Repositories.UnitOfWork
{
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Repositories.GenericRepository;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<Genero> _generoRepository;
        private IGenericRepository<PeliculaSerie> _peliculaSerieRepository;
        private IGenericRepository<Personaje> _personajeRepository;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Rol> _rolRepository;

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

        public IGenericRepository<User> UserRepository
        {
            get
            {
                return _userRepository ??= new GenericRepository<User>(_disneyDBContext);
            }
        }

        public IGenericRepository<Rol> RolRepository
        {
            get
            {
                return _rolRepository ??= new GenericRepository<Rol>(_disneyDBContext);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveAsync()
        {
            return await _disneyDBContext.SaveChangesAsync() > 0 ;
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
