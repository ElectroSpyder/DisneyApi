using DisneyApi.Core.Logic.EntitiesRepositories;
using DisneyApi.Core.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisneyApi.Core.Api.Auth
{
    [AllowAnonymous]
    [Route("[auth]")]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioRepository usuarioRepository;

        public LoginController(UsuarioRepository repository)
        {
            usuarioRepository = repository;
        }

        [HttpGet("/login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Usuario>>> GetById(Usuario usuario)
        {
            var result = usuarioRepository.GetByFunc(x => x.Email == usuario.Email && x.Password == usuario.Password).Result;
            await Task.Delay(500).ConfigureAwait(false);
            //return result;
            if (result == null) return BadRequest();
            if (result.Count == 0) return BadRequest();
            return Ok();
           

        }
    }
}
