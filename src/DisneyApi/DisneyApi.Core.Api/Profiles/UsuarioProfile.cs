namespace DisneyApi.Core.Api.Configuration
{
    using AutoMapper;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Models.Entities;

    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<LoginViewModel, User>();

            CreateMap<User, LoginViewModel>();
        }
    }
}
