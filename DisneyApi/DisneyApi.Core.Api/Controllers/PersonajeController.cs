namespace DisneyApi.Core.Api.Controllers
{
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class PersonajeController : ControllerBase
    {
        private readonly PersonajeRepository personajeRepository;
        public byte[] contentt { get; set; }
        public PersonajeController(PersonajeRepository repository) 
        {
            personajeRepository = repository;
        }

        [HttpGet("/characters")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Personaje>>> GetAll()
        {
            var result= personajeRepository.GetAll().Result;
            await Task.Delay(500).ConfigureAwait(false);
            return result;
        }

        [HttpPost("/Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<bool> Add(PersonajeViewModel personajeViewModel)
        {
            var personaje = new Personaje
            {
                Nombre = personajeViewModel.Nombre,
                Edad = personajeViewModel.Edad,
                Peso = personajeViewModel.Peso,
                Historia = personajeViewModel.Historia
            };

            //TODO : Probar si se obtiene el byte[] de la imagen
            //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.2

            using (var memoryStream = new MemoryStream())
            {
                await personajeViewModel.Image.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    contentt = memoryStream.ToArray();           
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }

            personaje.Imagen = new Imagen
            {
                Contentt = contentt
            };

            await personajeRepository.Add(personaje);
            await Task.Delay(500).ConfigureAwait(false);

            return true;

        }
        
    }
}
