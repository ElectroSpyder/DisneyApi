namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Models.Repository; 

    public class PersonajeRepository : Repository<Personaje, DisneyDBContext>
    {
        public PersonajeRepository(DisneyDBContext context): base(context)
        {

        }
    }
}
