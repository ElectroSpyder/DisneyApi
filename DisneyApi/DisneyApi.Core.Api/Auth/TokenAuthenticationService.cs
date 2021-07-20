namespace DisneyApi.Core.Api.Auth
{
    using DisneyApi.Core.Api.Configuration;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Logic.EntitiesRepositories;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly UsuarioRepository usuarioRepository;
        private readonly TokensKey tokensKey;
        public TokenAuthenticationService(UsuarioRepository repository, IOptions<TokensKey> tokens)
        {
            usuarioRepository = repository;
            tokensKey = tokens.Value;
        }
        public bool IsAuthenticated(LoginViewModel request, out string token)
        {
            token = string.Empty;
            var result = usuarioRepository.GetByFunc(x => x.Email == request.Email && x.Password == request.Password).Result;
            if (result.Count == 0) return false;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokensKey.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(tokensKey.Issuer, tokensKey.Audience, claims,
                                            expires: DateTime.Now.AddMinutes(tokensKey.AccessExpiration), 
                                            signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
