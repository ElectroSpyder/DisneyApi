using DisneyApi.Core.Models.Entities;
using DisneyApi.Core.Repositories.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisneyApi.Core.Api.Services
{
    public class GeneroService : IGeneroService
    {
        private IUnitOfWork _unitOfWork;

        public GeneroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Genero> AddGenero(Genero genero)
        {
            await _unitOfWork.GeneroRepository.Add(genero);
            _unitOfWork.Save();

            return genero;
        }

        public async Task<Genero> GetGeneroByIdAsync(int id)
        {
            return await _unitOfWork.GeneroRepository.Get(id);
        }

        public async Task<IEnumerable<Genero>> GetGenerosAsync()
        {
            return await _unitOfWork.GeneroRepository.GetAll();
        }
    }
}
