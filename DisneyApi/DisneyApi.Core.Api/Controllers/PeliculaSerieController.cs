namespace DisneyApi.Core.Api.Controllers
{
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class PeliculaSerieController : ControllerBase
    {
        private readonly PeliculaSerieRepository peliculaSerieRepository;

        public PeliculaSerieController(PeliculaSerieRepository repository)
        {
            peliculaSerieRepository = repository;
        }

        [HttpGet("/movies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PeliculaSerie>>> GetAll()
        {
            var result = peliculaSerieRepository.GetAll().Result;
            await Task.Delay(500).ConfigureAwait(false);
            return result;
        }

        [HttpPost("/movie/Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Add(PeliculaSerieViewModel peliculaSerieViewModel)
        {
            var pelicualSerie = new PeliculaSerie
            {
                Titulo = peliculaSerieViewModel.Titulo,
                FechaCreacion = peliculaSerieViewModel.FechaCreacion,
                Calificacion = peliculaSerieViewModel.Calificacion
            };

            await peliculaSerieRepository.Add(pelicualSerie);
            await Task.Delay(500).ConfigureAwait(false);

            return true;

        }
    }
}
