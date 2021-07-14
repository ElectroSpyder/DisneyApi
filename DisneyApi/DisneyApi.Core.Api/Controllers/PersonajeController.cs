namespace DisneyApi.Core.Api.Controllers
{
    using DisneyApi.Core.Api.Base;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Models.Repository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PersonajeController : BaseDisneyController<Personaje,PersonajeRepository>
    {       

        public PersonajeController(PersonajeRepository repository) : base(repository)
        {

        }     
          
        
    }
}
