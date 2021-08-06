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
        public async Task<ActionResult<List<ListPersonajeViewModel>>> GetAll(string name = null, int genre = 0, string order = null)
        {
            IList<Personaje> result = null;
            if (name == null && genre == 0)
            {
                result = await personajeRepository.GetAll();
            }
            else
            {
                if (name != null && genre != 0)
                {
                    result = personajeRepository.GetByFunc(x => x.IdGenero == genre && x.Nombre == name, order).ToList();
                }
                else
                {
                    if (genre != 0)
                    {
                        result = personajeRepository.GetByFunc(x => x.IdGenero == genre, order).ToList();
                    }
                    else
                    {
                        if (name != null)
                        {
                            result = personajeRepository.GetByFunc(x => x.Nombre == name, order).ToList();
                        }
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
                var entityExist =  personajeRepository.GetByFunc(x => x.Id == personajeViewModel.Id, null).ToList();
                if (!entityExist.Any()) return NotFound();

                _mapper.Map(personajeViewModel, entityExist[0]);

                if (await personajeRepository.Update(entityExist[0]) != null) return Ok(_mapper.Map<GeneroViewModel>(entityExist[0]));

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
                var entityExist = personajeRepository.GetByFunc(x => x.Id == personajeViewModel.Id, null).ToList();
                if (!entityExist.Any()) return NotFound();               
                            
                var result = await personajeRepository.Delete(entityExist[0].Id);
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
