using DisneyApi.Core.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Core.Api.Services.PeliculaSerie
{
    public class PeliculaSerieService : IPeliculaSerieService
    {
        private IUnitOfWork _unitOfWork;

        public PeliculaSerieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Models.Entities.PeliculaSerie> AddPeliculaSerie(Models.Entities.PeliculaSerie peliculaSerie)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Entities.PeliculaSerie> DeletePeliculaSerie(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Entities.PeliculaSerie>> GetPeliculaSerieAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Entities.PeliculaSerie> GetPeliculaSerieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Entities.PeliculaSerie> PutPeliculaSerie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
