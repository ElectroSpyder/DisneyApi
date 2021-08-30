namespace DisneyApi.Core.Api.Services.User
{
    using DisneyApi.Core.Repositories.UnitOfWork;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Api.Configuration;
    using System.IdentityModel.Tokens.Jwt;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Microsoft.Extensions.Configuration;

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMailService mailService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _configuration = configuration;
        }

        public async Task<Response> CreateUserAsync(LoginViewModel loginViewModel)
        {
            try
            {
                var result = await _unitOfWork.UserRepository.GetByFunc( x => x.Email == loginViewModel.Email);

                if (result.ToList().Count == 0)
                {
                    var user = new User
                    {
                        Email = loginViewModel.Email,
                        PasswordHash = loginViewModel.Password,
                        Storesalt = Guid.NewGuid().ToString(),
                        IdRol = 1
                    };
                    
                    var newUser =await _unitOfWork.UserRepository.Add(user);
                    if (newUser != null) { 
                        await _mailService.SendEmialAsync(newUser);

                        return new Response
                        {
                            SuccessStatusCode = true,
                            Status = "Ok",
                            Mensaje = $"Usuario creado, se envió un email a su correo {loginViewModel.Email} para validar"
                        };
                    }
                    else
                    {
                        return new Response
                        {
                            SuccessStatusCode = false,
                            Status = "Error Interno",
                            Mensaje = $"Ocurrió un Error al grabar el usuario {newUser.Email}"
                        };
                    }
                }
                else
                {
                    return new Response
                    {
                        SuccessStatusCode = false,
                        Status = "Advertencia",
                        Mensaje = $"Ya esta en uso el usuario {loginViewModel.Email}"
                    };
                }

                
            }
            catch (Exception ex)
            {

                return new Response
                {
                    SuccessStatusCode = false,
                    Status = "Error",
                    Mensaje = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public async Task<Response> LoginAsync(LoginViewModel loginViewModel)
        {
            try
            {
                var result = await _unitOfWork.UserRepository
                    .GetByFunc(x => x.Email == loginViewModel.Email 
                                 && x.PasswordHash == loginViewModel.Password);
                
                if (result.ToList().Count > 0)
                {
                    return new Response
                    {
                        SuccessStatusCode = true,
                        Status = "Token",
                        Mensaje = CreateToken(loginViewModel)
                    };
                }

                return new Response
                {
                    SuccessStatusCode = false,
                    Status = "Advertencia",
                    Mensaje = "Usuario inexistente"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    SuccessStatusCode = false,
                    Status = "Error",
                    Mensaje = $"Error inesperado: {ex.Message}"
                };
            }
        }

        private string CreateToken(LoginViewModel loginViewModel)
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

            var miToken = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return miToken.ToString();
        }
    }
}
