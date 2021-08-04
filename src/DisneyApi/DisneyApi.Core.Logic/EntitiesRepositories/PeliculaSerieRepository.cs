namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Models.Repository;

    public class PeliculaSerieRepository: Repository<PeliculaSerie, DisneyDBContext>
    {
        public PeliculaSerieRepository( DisneyDBContext context) : base(context)
        {

        }
    }
}
