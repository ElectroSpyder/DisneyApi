namespace DisneyApi.Core.Api.Controllers
{
    using AutoMapper;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.LogicRepositories.Repository;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]    
    public class PeliculaSerieController : ControllerBase
    {
        private readonly IPeliculaSerieRepository _peliculaSerieRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IPersonajeRepository _personajeRepository;
        private readonly IMapper _mapper;

        public PeliculaSerieController(IPeliculaSerieRepository repository, IMapper mapper, 
            IGeneroRepository generoRepository, 
            IPersonajeRepository personajeRepository)
        {
            _peliculaSerieRepository = repository;
            _mapper = mapper;
            _generoRepository = generoRepository;
            _personajeRepository = personajeRepository;
        }

        [HttpGet("/movies")]
        public async Task<ActionResult<List<PeliculaSerieViewModel>>> GetAll(string name, int genre, string order)
        {
            try
            {
                List<PeliculaSerie> result = null;
                if (name == null && genre == 0)
                {
                    result = await _peliculaSerieRepository.GetAll();
                }
                else
                {
                    if (name != null && genre != 0)
                    {
                        result = (await _peliculaSerieRepository.GetByFunc(x => x.IdGenero == genre && x.Titulo == name, order)).ToList();
                    }
                    else
                    {
                        if (genre != 0)
                        {
                            result = (await _peliculaSerieRepository.GetByFunc(x => x.IdGenero == genre, order)).ToList();
                        }
                        else
                        {
                            if (name != null)
                            {
                                result = (await _peliculaSerieRepository.GetByFunc(x => x.Titulo == name, order)).ToList();
                            }
                        }
                    }

                }

                if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error al devolver datos");

                var listPelicualSerie = _mapper.Map<List<PeliculaSerieViewModel>>(result);

                return Ok(listPelicualSerie);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al devolver datos, error : {ex.Message}");
            }

        }

        [HttpPost()]
        public async Task<ActionResult<PeliculaSerieAllEntitiesViewModel>> Add(PeliculaSerieAllEntitiesViewModel peliculaSerieViewModel)
        {
            try
            {
                var newGenero = new Genero { Nombre = peliculaSerieViewModel.GeneroNombre };
                if (!string.IsNullOrEmpty(peliculaSerieViewModel.GeneroNombre))
                {
                    
                    var generoFind = (await _generoRepository.GetByFunc(x => x.Nombre == peliculaSerieViewModel.GeneroNombre)).ToList();

                    if (generoFind == null || generoFind.Count() == 0) {
                        newGenero = await _generoRepository.Add(newGenero);
                    }
                    else
                    {
                        newGenero = generoFind[0];
                    }

                }
               
                var entityMap = new PeliculaSerie
                {
                    Genero = newGenero,
                    FechaCreacion = peliculaSerieViewModel.FechaCreacion,
                    Calificacion = peliculaSerieViewModel.Calificacion,
                    Titulo = peliculaSerieViewModel.Titulo
                };

                var personajes = new List<Personaje>();
                if (peliculaSerieViewModel.Personajes != null)
                {
                    if (peliculaSerieViewModel.Personajes.Count > 0)
                    {
                        foreach (var pers in peliculaSerieViewModel.Personajes)
                        {
                            var resultPersonas = (await _personajeRepository.GetByFunc(x => x.Nombre == pers.Nombre)).ToList();
                            if (resultPersonas != null || resultPersonas.Count > 0)
                            {
                                foreach (var item in resultPersonas)
                                {
                                    await _personajeRepository.Add(item);
                                    personajes.Add(item);
                                }
                            }
                            else
                            {
                                personajes.Add(_mapper.Map<Personaje>(pers));
                            }
                        }
                    }
                }
              

                entityMap.Personajes = personajes;

                var result = await _peliculaSerieRepository.Add(entityMap);

                return Ok(_mapper.Map<PeliculaSerieAllEntitiesViewModel>(result));

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al devolver datos, error : {ex.Message}");
            }

        }

        [HttpPut()]
        public async Task<ActionResult<bool>> Update(PeliculaSerieAllEntitiesViewModel peliculaSerieViewModel)
        {
            try
            {
               
                var oldPelicula = (await _peliculaSerieRepository.GetByFunc(x => x.Titulo == peliculaSerieViewModel.Titulo)).ToList();
                
                
                var miGenero = _mapper.Map<GeneroViewModel>(oldPelicula[0].Genero);
                var nuevoGenero = await _generoRepository.Update(_mapper.Map<Genero>(miGenero));

                var entityMap = _mapper.Map<PeliculaSerie>(peliculaSerieViewModel);
                entityMap.Genero = nuevoGenero;

                if (entityMap == null) return NotFound();

                var result = await _peliculaSerieRepository.Update(entityMap);

                return Ok(_mapper.Map<PeliculaSerieAllEntitiesViewModel>(result));
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al devolver datos, error : {ex.Message}");
            }           

        }

        [HttpDelete()]
        public async Task<ActionResult<bool>> Delete(PeliculaSerieViewModel peliculaSerieViewModel)
        {
            var entityMap = _mapper.Map<PeliculaSerie>(peliculaSerieViewModel);
            if (entityMap == null) return NotFound();

            await _peliculaSerieRepository.Delete(entityMap.Id);

            return Ok(true);

        }
    }
}
