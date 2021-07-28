namespace DisneyApi.Core.Api.Configuration
{
    using AutoMapper;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Models.Entities;

    public class GeneroProfile : Profile
    {
        public GeneroProfile()
        {
            CreateMap<Genero, GeneroViewModel>();
            CreateMap<GeneroViewModel, Genero>();
        }
    }
}
