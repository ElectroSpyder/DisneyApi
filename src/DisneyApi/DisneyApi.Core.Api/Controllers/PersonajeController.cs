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
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //[Authorize]
    [ApiController]
    [Route("[controller]")]    
    public class PersonajeController : ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;
        private readonly IPeliculaSerieRepository _peliculaSerieRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public PersonajeController(IPersonajeRepository repository, 
            IMapper mapper, 
            IPeliculaSerieRepository peliculaSerieRepository,
            IGeneroRepository generoRepository) 
        {
            _personajeRepository = repository;
            _mapper = mapper;
            _peliculaSerieRepository = peliculaSerieRepository;
            _generoRepository = generoRepository;
        }

        [HttpGet("/characters")]
        public async Task<ActionResult<List<ListPersonajeViewModel>>> GetAll()
        {
            try
            {
                var result = await _personajeRepository.GetAll(); ;


                if (result == null || result.Count == 0) return StatusCode(StatusCodes.Status500InternalServerError, "Error al devolver datos");

                var listPersonaje = _mapper.Map<List<ListPersonajeViewModel>>(result);
                return listPersonaje;
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al devolver datos, error : {ex.Message}");
            }

        }

        [HttpGet("/{name}")]
        public async Task<ActionResult<PersonajeViewModel>> Get(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return StatusCode(StatusCodes.Status500InternalServerError, "Nombre vacio");

                var result = await  _personajeRepository.GetByFunc(x => x.Nombre == name);

                if (result == null)
                    return NotFound($"Persnaje {name} no encontrado");

                return _mapper.Map<PersonajeViewModel>(result.ToList()[0]);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al devolver datos, error : {ex.Message}");
            }
        }


        [HttpPost("/personaje/Add")]
        public async Task<ActionResult<PersonajeViewModel>> Add(PersonajeAllEntitiesViewModel personajeViewModel)
        {
            try
            {
                var peliculas = new List<PeliculaSerie>();
                if (personajeViewModel.PeliculasSeries.Count > 0)
                {
                    foreach (var peli in personajeViewModel.PeliculasSeries)
                    {
                        var result = (await _peliculaSerieRepository.GetByFunc(x => x.Titulo == peli.Titulo)).ToList();
                        if (result.Count == 0 || result == null)
                        {
                            var newGenero = new Genero { Nombre = peli.GeneroNombre };

                            var generoFind = (await _generoRepository.GetByFunc(x => x.Nombre == peli.GeneroNombre)).ToList();

                            if (generoFind == null || generoFind.Count() == 0)
                            {
                                newGenero = await _generoRepository.Add(newGenero);
                            }
                            else
                            {
                                newGenero = generoFind[0];
                            }

                            var newPeli = _mapper.Map<PeliculaSerie>(peli);
                            newPeli.Genero = newGenero;
                            peliculas.Add(newPeli);
                        }
                        else
                        {
                            foreach (var item in result)
                            {
                                peliculas.Add(item);
                            }
                        }
                    } 
                }

                var entityMap = _mapper.Map<Personaje>(personajeViewModel);
                entityMap.PeliculasSeries = peliculas;

                if (await _personajeRepository.Add(entityMap) == null)
                    return BadRequest($"Ocurrio un error al guardar el personaje {personajeViewModel.Nombre}");

               
                return Ok(personajeViewModel);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            

        }

        [HttpPut("/personaje/update/{nombre}")]
        public async Task<ActionResult<PersonajeViewModel>> Put(string nombre, PersonajeViewModel personajeViewModel)
        {
            try
            {
                var entityExist = await _personajeRepository.GetByFunc(x => x.Nombre == nombre, null);
                var personajeExist = new Personaje();
                if (!entityExist.ToList().Any())
                {                    
                        return NotFound();                    
                }
                personajeExist = entityExist.ToList()[0];

                _mapper.Map(personajeViewModel, personajeExist);

                if (await _personajeRepository.Update(personajeExist) != null) 
                    return Ok(_mapper.Map<PersonajeViewModel>(personajeExist));

                return StatusCode(StatusCodes.Status500InternalServerError, "Error al guardar");

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("/personaje/delete/{nombre}")]
        public async Task<ActionResult<PersonajeViewModel>> Delete(string nombre, PersonajeViewModel personajeViewModel)
        {
            try
            {
                var entityExist = await _personajeRepository.GetByFunc(x => x.Nombre == nombre, null);
                if (entityExist == null) return NotFound();

                var personajeExist = entityExist.ToList()[0];

                var result = await _personajeRepository.Delete(personajeExist.Id);
                if (result != null)
                {
                    return Ok(_mapper.Map(result,personajeViewModel));
                }

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound();
        }

    }
}
