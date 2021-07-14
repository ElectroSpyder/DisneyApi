using DisneyApi.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyApi.Core.Models.Repository
{
    //https://code-maze.com/async-generic-repository-pattern/
    public interface IPeliculaSerieRepository : IRepository<PeliculaSerie>
    {
        Task<IEnumerable<PeliculaSerie>> GetAllAsync();
        Task<PeliculaSerie> GetByIdAsync(int id);
        void CreatePeliculaSerie(PeliculaSerie peliculaSerie);
        void UpdatePeliculaSerie(PeliculaSerie peliculaSerie);
    }
}
