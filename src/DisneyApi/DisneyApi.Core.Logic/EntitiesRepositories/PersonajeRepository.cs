namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Logic.Repository;
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;

    public class PersonajeRepository : Repository<Personaje, DisneyDBContext>
    {
        public PersonajeRepository(DisneyDBContext context): base(context)
        {

        }
    }
}
