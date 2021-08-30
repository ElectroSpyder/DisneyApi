using DisneyApi.Core.Api.Services.User;
using DisneyApi.Core.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using System.Threading.Tasks;

namespace DisneyApi.Core.Api.Controllers
{
    public class AuthenticateController : Controller
    {
        
        //private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;
       // private readonly UserManager<User> _userManager;
        //private readonly IMailService mailService;

        private readonly IUserService _userService;
        public AuthenticateController(IUserService userService)
        {
           // _configuration = configuration;
            //_mapper = mapper;
           // _userManager = userManager;
          //  mailService = service;
            _userService = userService;
        }
        // GET: AuthenticateController

        [HttpPost]
        [Route("/auth/register")]
        public async Task<IActionResult> Register([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var result = await _userService.CreateUserAsync(loginViewModel);

                if (result.SuccessStatusCode)
                {
                    return Ok(result);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, result);
                                               
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
                var result = await _userService.LoginAsync(loginViewModel);

                if (result.SuccessStatusCode)
                {
                    return Ok(result.Mensaje);   //retorna el token
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
