namespace DisneyApi.Core.Api.Controllers
{
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GeneroController : ControllerBase
    {

        private readonly GeneroRepository generoRepository;
        public byte[] contentt { get; set; }
        public GeneroController(GeneroRepository repository)
        {
            generoRepository = repository;
        }

        [HttpGet("/generos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Genero>>> GetAll()
        {
            var result = generoRepository.GetAll().Result;
            await Task.Delay(500).ConfigureAwait(false);
            return result;
        }

        [HttpPost("/genero/Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Add(GeneroViewModel generoViewModel)
        {
            var genero = new Genero
            {
               Nombre = generoViewModel.Nombre
            };

            await generoRepository.Add(genero);
            await Task.Delay(500).ConfigureAwait(false);

            return true;

        }
    }
}
