using DisneyApi.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisneyApi.Core.Api.Services
{
    public interface IGeneroService
    {
        Task<Genero> AddGenero(Genero genero);
        Task<Genero> GetGeneroByIdAsync(int id);
        Task<IEnumerable<Genero>> GetGenerosAsync();

        Task<Genero> DeleteGenero(string nombre);
        Task<Genero> PutGenero(int id);
    }
}