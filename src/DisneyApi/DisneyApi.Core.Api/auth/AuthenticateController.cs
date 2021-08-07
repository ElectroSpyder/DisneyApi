using AutoMapper;
using DisneyApi.Core.Api.Configuration;
using DisneyApi.Core.Api.ViewModels;
using DisneyApi.Core.Email;
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
using System.Linq;
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
        private readonly UserManager<User> _userManager;
        private readonly IMailService mailService;
        public AuthenticateController(UsuarioRepository usuarioRepository,          
            IConfiguration configuration,
            IMapper mapper, UserManager<User> userManager,
            IMailService service)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            mailService = service;
        }
        // GET: AuthenticateController
       
        [HttpPost]
        [Route("/auth/register")]
        public async Task<IActionResult> Register([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                //var usuarioExist = _usuarioRepository.GetByFunc(x => x.Email == loginViewModel.UserName, null).ToList();
                var usuarioExist = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (usuarioExist != null )
                {
                    //if(usuarioExist.Count() > 0)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Mensaje = $"Ya esta en uso el usuario {loginViewModel.UserName}" });
                }

                var usuario = new User()
                {
                    Email = loginViewModel.Email,
                    UserName = loginViewModel.UserName,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(usuario, loginViewModel.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response
                        {
                            Status = "Error",
                            Mensaje = $"Leer los mensajes: {string.Join(";", result.Errors.Select(x => x.Description))}"
                        });
                }
                if (result != null) {
                    /* var sendEmail = new SendEmail(_configuration["SendEmailKey: Key"], usuario.Email);  //"SendEmailKey": { "key"
                     await sendEmail.Send();*/
                    await mailService.SendEmialAsync(usuario);
                    return Ok(new Response { Status = "Success", Mensaje = "Usuario creado satisfactoriamente, se envio email para validar" });
                }
                

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Mensaje = "Ocurrio un problema al crear el sistema" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var result = await _userManager.FindByNameAsync(loginViewModel.UserName);

                if (result != null)
                {
                    return Ok(CreateToken(loginViewModel));   //retorna el token
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

        private IActionResult CreateToken(LoginViewModel loginViewModel)
        {
            var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginViewModel.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }

    }
}
