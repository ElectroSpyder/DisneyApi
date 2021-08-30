namespace DisneyApi.Core.Api.Services.PeliculaSerie
{
    using DisneyApi.Core.Models.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPeliculaSerieService
    {
        Task<PeliculaSerie> AddPeliculaSerie(PeliculaSerie peliculaSerie);
        Task<PeliculaSerie> GetPeliculaSerieByIdAsync(int id);
        Task<IEnumerable<PeliculaSerie>> GetPeliculaSerieAsync();

        Task<PeliculaSerie> DeletePeliculaSerie(string nombre);
        Task<PeliculaSerie> PutPeliculaSerie(int id);
    }
}
