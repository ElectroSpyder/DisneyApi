using DisneyApi.Core.Models.Entities;
using DisneyApi.Core.Repositories.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Genero> DeleteGenero(string nombre)
        {
            var genero = await _unitOfWork.GeneroRepository.GetByFunc(x => x.Nombre == nombre);
            if (genero != null)
            {
                var generoToDelete = genero.ToList()[0];
                return await _unitOfWork.GeneroRepository.Delete(generoToDelete.Id);
            }

            return null;
        }

        public async Task<Genero> GetGeneroByIdAsync(int id)
        {
            return await _unitOfWork.GeneroRepository.Get(id);
        }

        public async Task<IEnumerable<Genero>> GetGenerosAsync()
        {
            return await _unitOfWork.GeneroRepository.GetAll();
        }

        public async Task<Genero> PutGenero(int id)
        {
            var oldModel = (await _unitOfWork.GeneroRepository.GetByFunc(x => x.Id == id, null)).ToList();
            if(oldModel != null)
            {
                if(oldModel.Any())
                {
                    return await _unitOfWork.GeneroRepository.Update(oldModel[0],id);
                }
            }

            return null;
        }
    }
}
