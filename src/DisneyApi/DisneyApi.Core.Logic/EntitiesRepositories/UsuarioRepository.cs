namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Logic.Repository;
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;

    public class UsuarioRepository : Repository<User,UserDbContext>
    {        
        public UsuarioRepository(UserDbContext context): base(context)
        {

        }
               
    }
}
