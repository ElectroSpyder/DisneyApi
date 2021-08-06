namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Logic.Repository;
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;

    public class GeneroRepository: Repository<Genero, DisneyDBContext>
    {

        public GeneroRepository(DisneyDBContext context) : base(context)
        {

        }
    }
}
