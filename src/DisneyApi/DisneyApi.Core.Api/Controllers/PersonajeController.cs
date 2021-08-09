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
    [Route("[controller]")]    
    public class PersonajeController : ControllerBase
    {
        private readonly PersonajeRepository personajeRepository;
        private readonly IMapper _mapper;
        public PersonajeController(PersonajeRepository repository, IMapper mapper) 
        {
            personajeRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("/characters")]
        public async Task<ActionResult<List<ListPersonajeViewModel>>> GetAll()
        {
            IList<Personaje> result = await personajeRepository.GetAll(); ;
            
            
            if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error al devolver datos");

            var listPersonaje = _mapper.Map<List<ListPersonajeViewModel>>(result);
            return listPersonaje;
        }

        [HttpGet("/{name}")]
        public async Task<ActionResult<PersonajeViewModel>> Get(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return StatusCode(StatusCodes.Status500InternalServerError, "Nombre vacio");

                var result = await  personajeRepository.GetByFunc(x => x.Nombre == name);

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
        public async Task<PersonajeViewModel> Add(PersonajeViewModel personajeViewModel)
        {
            var entityMap = _mapper.Map<Personaje>(personajeViewModel);
            await personajeRepository.Add(entityMap);
            
            return personajeViewModel;

        }

        [HttpPut("/personaje/update")]
        public async Task<ActionResult<PersonajeViewModel>> Put(PersonajeViewModel personajeViewModel)
        {
            try
            {
                var entityExist = await personajeRepository.GetByFunc(x => x.Id == personajeViewModel.Id, null);
                var personajeExist = new Personaje();
                if (!entityExist.ToList().Any())
                {                    
                        return NotFound();                    
                }
                personajeExist = entityExist.ToList()[0];

                _mapper.Map(personajeViewModel, personajeExist);

                if (await personajeRepository.Update(personajeExist) != null) 
                    return Ok(_mapper.Map<GeneroViewModel>(personajeExist));

                return StatusCode(StatusCodes.Status500InternalServerError, "Error al guardar");

            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("/personaje/delete")]
        public async Task<ActionResult<PersonajeViewModel>> Delete(PersonajeViewModel personajeViewModel)
        {
            try
            {
                var entityExist = await personajeRepository.GetByFunc(x => x.Id == personajeViewModel.Id, null);
                if (entityExist == null) return NotFound();

                var personajeExist = entityExist.ToList()[0];

                var result = await personajeRepository.Delete(personajeExist.Id);
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
