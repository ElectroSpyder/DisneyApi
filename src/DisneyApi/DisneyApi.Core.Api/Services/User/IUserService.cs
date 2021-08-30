namespace DisneyApi.Core.Api.Services.User
{
    using DisneyApi.Core.Api.ViewModels;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<Response> CreateUserAsync(LoginViewModel loginViewModel);
        Task<Response> LoginAsync(LoginViewModel loginViewModel);
    }
}
