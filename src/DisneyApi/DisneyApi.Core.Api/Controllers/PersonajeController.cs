namespace DisneyApi.Core.Api.Controllers
{
    using AutoMapper;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        public async Task<ActionResult<List<ListPersonajeViewModel>>> GetAll(string name = null, int genre = 0, string order = null)
        {
            IList<Personaje> result = null;
            if (name == null && genre == 0)
            {
                result = await personajeRepository.GetAll();
            }
            else
            {
                if (genre != 0)
                {
                    result = await personajeRepository.GetByFunc(x => x.IdGenero == genre, order);
                }
                else
                {
                    if (name != null)
                    {
                        result = await personajeRepository.GetByFunc(x => x.Nombre == name, order);
                    }
                }                
            }
            
            if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error al devolver datos");

            var listPersonaje = _mapper.Map<List<ListPersonajeViewModel>>(result);
            return listPersonaje;
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
                if (entityExist == null) return NotFound();
                if (entityExist.Count == 0) return StatusCode(StatusCodes.Status204NoContent, "No hay items para mostrar");

                var entityMap = _mapper.Map<Personaje>(personajeViewModel);
                var result = await personajeRepository.Update(entityMap);
                if (result != null)
                {
                    return Ok(result);
                }


            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NotFound();
        }

        [HttpDelete("/personaje/delete")]
        public async Task<ActionResult<PersonajeViewModel>> Delete(PersonajeViewModel personajeViewModel)
        {
            try
            {
                var entityExist = await personajeRepository.GetByFunc(x => x.Id == personajeViewModel.Id, null);
                if (entityExist == null) return NotFound();
                if (entityExist.Count == 0) return StatusCode(StatusCodes.Status204NoContent, "No hay items para mostrar");

                var entityMap = _mapper.Map<Personaje>(personajeViewModel);
                var result = await personajeRepository.Delete(entityMap.Id);
                if (result != null)
                {
                    return Ok(result);
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
