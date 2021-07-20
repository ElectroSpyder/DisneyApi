namespace DisneyApi.Core.Api.Auth
{
    using DisneyApi.Core.Api.ViewModels;

    public interface IAuthenticateService
    {

        bool IsAuthenticated(LoginViewModel request, out string token);
    }
}
