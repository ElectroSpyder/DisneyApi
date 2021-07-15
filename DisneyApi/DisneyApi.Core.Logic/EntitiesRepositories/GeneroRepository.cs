namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Models.Repository;

    public class GeneroRepository: Repository<Genero, DisneyDBContext>
    {

        public GeneroRepository(DisneyDBContext context) : base(context)
        {

        }
    }
}
