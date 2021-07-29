namespace DisneyApi.Core.Api.Configuration
{
    using AutoMapper;
    using DisneyApi.Core.Api.ViewModels;
    using DisneyApi.Core.Models.Entities;

    public class PersonajeProfile : Profile
    {
        public PersonajeProfile()
        {
            CreateMap<Personaje, PersonajeViewModel>();
            CreateMap<PersonajeViewModel, Personaje>();
            CreateMap<Personaje, ListPersonajeViewModel>();
            CreateMap<ListPersonajeViewModel, Personaje>();
        }
    }
}
