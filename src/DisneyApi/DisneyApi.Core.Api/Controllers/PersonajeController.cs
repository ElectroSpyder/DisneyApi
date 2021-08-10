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
        private readonly IMapper _mapper;

        public PersonajeController(IPersonajeRepository repository, IMapper mapper) 
        {
            _personajeRepository = repository;
            _mapper = mapper;
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
        public async Task<ActionResult<PersonajeViewModel>> Add(PersonajeViewModel personajeViewModel)
        {
            try
            {
               
                var entityMap = _mapper.Map<Personaje>(personajeViewModel);
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
