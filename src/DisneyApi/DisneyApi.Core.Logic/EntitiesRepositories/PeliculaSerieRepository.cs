namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Logic.Repository;
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;

    public class PeliculaSerieRepository: Repository<PeliculaSerie, DisneyDBContext>
    {
        public PeliculaSerieRepository( DisneyDBContext context) : base(context)
        {

        }
    }
}
