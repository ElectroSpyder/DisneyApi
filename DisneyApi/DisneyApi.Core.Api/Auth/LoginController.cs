using DisneyApi.Core.Api.ViewModels;
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
        private readonly IAuthenticateService authenticate;
        public LoginController(UsuarioRepository repository, IAuthenticateService authenticateService)
        {
            usuarioRepository = repository;
            authenticate = authenticateService;
        }

        [HttpGet("/login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Usuario>>> GetById(LoginViewModel usuario)
        {
            var result = usuarioRepository.GetByFunc(x => x.Email == usuario.Email && x.Password == usuario.Password).Result;
            await Task.Delay(500).ConfigureAwait(false);
            //return result;
            if (result == null) return BadRequest();
            if (result.Count == 0) return BadRequest();
            return Ok();
           

        }

        [HttpPost, Route("/auth/login")]
        public ActionResult RequestToken([FromBody] LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            if (authenticate.IsAuthenticated(request, out string token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");

        }

    }
}
