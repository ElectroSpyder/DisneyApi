namespace DisneyApi.Core.Api.Controllers
{
    using AutoMapper;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using DisneyApi.Core.Models.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class GeneroController : ControllerBase
    {

        private readonly GeneroRepository generoRepository;
        public byte[] Contentt { get; set; }
        private readonly IMapper _autoMapper;

        public GeneroController(GeneroRepository repository, IMapper mapper)
        {
            generoRepository = repository;
            _autoMapper = mapper;
        }       

        [HttpPost()]       
        public async Task<ActionResult<bool>> Add(GeneroViewModel generoViewModel)
        {
            try
            {
                var genero = new Genero
                {
                    Nombre = generoViewModel.Nombre
                };

                var generoCreat = await generoRepository.Add(genero);
                if (generoCreat == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo agregar el genero");
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneroViewModel>> Get(int id)
        {
            try
            {
                var genero = await generoRepository.Get(id);

                if (genero == null) return NotFound();

                return Ok(genero);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("generos")]
        public async Task<ActionResult<List<GeneroViewModel>>> GetAll()
        {
            try
            {
                var listGenero = await generoRepository.GetAll();

                if (listGenero == null) return NotFound();
                if (listGenero.Count == 0) return StatusCode(StatusCodes.Status204NoContent, "No se encontraron Generos para visualizar");
                
                var listGeneroviewModel = _autoMapper.Map<List<GeneroViewModel>>(listGenero);
                return Ok(listGeneroviewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{nombre}")]
        public async Task<ActionResult<bool>> Delete(string nombre)
        {
            try
            {
                var generoToDelete = generoRepository.GetByFunc(x => x.Nombre == nombre, null).ToList();
                if (!generoToDelete.Any()) return NotFound();

                var result = await generoRepository.Delete(generoToDelete[0].Id);

                if (result != null) return Ok(true);

                return StatusCode(StatusCodes.Status500InternalServerError, "Algo ocurrio que no se pudo borrar el genero");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<GeneroViewModel>> Put(GeneroViewModel generoViewModel)
        {
            try
            {
                var oldModel = generoRepository.GetByFunc(x => x.Id == generoViewModel.Id, null).ToList();
                if (!oldModel.Any()) return NotFound();

                _autoMapper.Map(generoViewModel, oldModel[0]);

               if(await generoRepository.Update(oldModel[0]) != null) return Ok(_autoMapper.Map<GeneroViewModel>(oldModel[0]));

                return StatusCode(StatusCodes.Status500InternalServerError, "Error al guardar");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
