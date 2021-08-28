using AutoMapper;
using DisneyApi.Core.Api.Configuration;
using DisneyApi.Core.Api.Controllers;
using DisneyApi.Core.Api.ViewModels;
using DisneyApi.Core.LogicRepositories.Repository;
using DisneyApi.Core.Models.Context;
using DisneyApi.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DisneyApi.Core.Test
{
    public class ControllerTest
    {
        /*
        private readonly Mock<IPeliculaSerieRepository> _peliculaRepository;
        private readonly Mock<IPersonajeRepository> _personajeRepository;
        private readonly Mock<IGeneroRepository> _generoRepository;
        private readonly PeliculaSerieController peliculaSerieController;        

        public ControllerTest()
        {
            _peliculaRepository = new Mock<IPeliculaSerieRepository>();
            _personajeRepository = new Mock<IPersonajeRepository>();
            _generoRepository = new Mock<IGeneroRepository>();
            

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PeliculaSerieProfile()); 
                cfg.AddProfile(new PersonajeProfile());
                cfg.AddProfile(new GeneroProfile());
            });
            var mapper = mockMapper.CreateMapper();


            peliculaSerieController = new PeliculaSerieController(
                _peliculaRepository.Object, mapper,
                _generoRepository.Object, _personajeRepository.Object);
            
        }
        [Fact]
        public void ReturnOkWhenSendValidModel()
        {
            
            //Arrange
            var model = new PeliculaSerieAllEntitiesViewModel
            {
                Titulo = "Test Titulo",
                Calificacion = 2,
                FechaCreacion = DateTime.Parse("2010-02-25")
                 
            };
                        
            //Act
            var result = peliculaSerieController.Add(model);

            var objetoResult = result.Result.Result as OkObjectResult;          

            //Assert
            Assert.IsType<OkObjectResult>(objetoResult);
           
        }

        [Fact]
        public void ReturnGetAllActionResult()
        {
            //Arrange
           
            _peliculaRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<PeliculaSerie>() 
            { 
                new PeliculaSerie { Id = 1, Titulo = "Test 1", Genero = new Genero{ Id=1, Nombre="Ciencia Ficcion" } },
                new PeliculaSerie { Id = 2, Titulo = "Test 2", Genero = new Genero{ Id=1, Nombre="Ciencia Ficcion" } },
                new PeliculaSerie { Id = 3, Titulo = "Test Titulo", Genero = new Genero{ Id=1, Nombre="Ciencia Ficcion" } }
            });
           
            //Act
            var result = peliculaSerieController.GetAll(null, 0, null);

            var objetoResult = result.Result.Result as OkObjectResult;

            var listado = objetoResult.Value as List<PeliculaSerieViewModel>;
            //Assert
            Assert.NotEmpty(listado);
        }

        */
    }
}
