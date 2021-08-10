using AutoMapper;
using DisneyApi.Core.Api.ViewModels;
using DisneyApi.Core.Models.Entities;

namespace DisneyApi.Core.Api.Configuration
{


    public class PeliculaSerieProfile : Profile
    {
        public PeliculaSerieProfile()
        {
            CreateMap<PeliculaSerie, PeliculaSerieViewModel>();
            CreateMap<PeliculaSerieViewModel, PeliculaSerie>();
        }
    }
}
