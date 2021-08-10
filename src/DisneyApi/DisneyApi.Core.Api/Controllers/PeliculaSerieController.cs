namespace DisneyApi.Core.Api.Controllers
{
    using AutoMapper;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]    
    public class PeliculaSerieController : ControllerBase
    {
        private readonly PeliculaSerieRepository peliculaSerieRepository;
        private readonly IMapper _mapper;
        public PeliculaSerieController(PeliculaSerieRepository repository, IMapper mapper)
        {
            peliculaSerieRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("/movies")]
        public async Task<ActionResult<List<PeliculaSerieViewModel>>> GetAll(string name, int genre, string order)
        {
            List<PeliculaSerie> result = null;
            if (name == null && genre == 0)
            {
                result = await peliculaSerieRepository.GetAll();
            }
            else
            {
                if (name != null && genre != 0)
                {
                    result = ( await peliculaSerieRepository.GetByFunc(x => x.IdGenero == genre && x.Titulo == name, order)).ToList();
                }
                else
                {
                    if (genre != 0)
                    {
                        result =(await peliculaSerieRepository.GetByFunc(x => x.IdGenero == genre, order)).ToList();
                    }
                    else
                    {
                        if (name != null)
                        {
                            result = (await peliculaSerieRepository.GetByFunc(x => x.Titulo == name, order)).ToList();
                        }
                    }
                }

            }

            if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error al devolver datos");

            var listPelicualSerie = _mapper.Map<List<PeliculaSerieViewModel>>(result);
            return listPelicualSerie;


        }

        [HttpPost()]
        public async Task<ActionResult<bool>> Add(PeliculaSerieViewModel peliculaSerieViewModel)
        {
            var entityMap = _mapper.Map<PeliculaSerie>(peliculaSerieViewModel);

            await peliculaSerieRepository.Add(entityMap);

            return Ok(true);

        }

        [HttpPut()]
        public async Task<ActionResult<bool>> Update(PeliculaSerieViewModel peliculaSerieViewModel)
        {
            var entityMap = _mapper.Map<PeliculaSerie>(peliculaSerieViewModel);
            if(entityMap == null) return NotFound();
            
            await peliculaSerieRepository.Add(entityMap);

            return Ok(true);

        }

        [HttpDelete()]
        public async Task<ActionResult<bool>> Delete(PeliculaSerieViewModel peliculaSerieViewModel)
        {
            var entityMap = _mapper.Map<PeliculaSerie>(peliculaSerieViewModel);
            if (entityMap == null) return NotFound();

            await peliculaSerieRepository.Delete(entityMap.Id);

            return Ok(true);

        }
    }
}
