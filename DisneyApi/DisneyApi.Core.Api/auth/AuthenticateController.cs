using AutoMapper;
using DisneyApi.Core.Api.ViewModels;
using DisneyApi.Core.Logic.EntitiesRepositories;
using DisneyApi.Core.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DisneyApi.Core.Api.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticateController(UsuarioRepository usuarioRepository,          
            IConfiguration configuration,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        // GET: AuthenticateController
       
        [HttpPost]
        [Route("/auth/register")]
        public async Task<IActionResult> Register([FromBody] LoginViewModel usuarioVM)
        {
            try
            {
                var usuarioExist = await _usuarioRepository.GetByFunc(x => x.Email == usuarioVM.Email);

                if (usuarioExist != null)
                {
                    if (usuarioExist.Count > 0)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Mensaje = "Ya esta en uso el Correo" });
                }

                var usuarioList = _mapper.Map<Usuario[]>(usuarioVM);
                var usuario = new Usuario();

                foreach (var item in usuarioList)
                {
                    usuario = item;
                }

                var result = await _usuarioRepository.Add(usuario);

                if (result != null) return Ok(new Response { Status = "Success", Mensaje = "Usuario creado satisfactoriamente" });

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Mensaje = "Ocurrio un problema al crear el sistema" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var result = await _usuarioRepository.GetByFunc(x => x.Email == model.Email && x.Password == model.Password);

                if (result != null)
                {
                    if (result.Count > 0)
                    {
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, model.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT: SecretKey"]));

                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT: ValidIssuer"],
                            audience: _configuration["JWT: ValidAudience"],
                            expires: DateTime.Now.AddHours(3),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
                        
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
