namespace DisneyApi.Core.Api.Base
{
    using DisneyApi.Core.Models.Repository;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class BaseDisneyController<T, TRepository> : ControllerBase
                                                    where T : class
                                                    where TRepository : IRepository<T>
    {
        private readonly TRepository _repository;

        public BaseDisneyController(TRepository repository)
        {
            _repository = repository;
        }        
       
        [HttpGet("/characters")]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            return await _repository.GetAll();
        }
    }
}
