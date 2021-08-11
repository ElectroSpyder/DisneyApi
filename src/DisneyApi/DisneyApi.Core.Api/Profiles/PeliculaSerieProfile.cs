using AutoMapper;
using DisneyApi.Core.Api.ViewModels;
using DisneyApi.Core.Models.Entities;

namespace DisneyApi.Core.Api.Configuration
{


    public class PeliculaSerieProfile : Profile
    {
        public PeliculaSerieProfile()
        {
            CreateMap<PeliculaSerie, PeliculaSerieViewModel>()
                .ForMember(x=> x.GeneroNombre , p=> p.MapFrom(m => m.Genero.Nombre));
            CreateMap<PeliculaSerieViewModel, PeliculaSerie>();

            CreateMap<PeliculaSerie, PeliculaSerieAllEntitiesViewModel>();
            CreateMap<PeliculaSerieAllEntitiesViewModel, PeliculaSerie>();
        }
    }
}
