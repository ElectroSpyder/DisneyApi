namespace DisneyApi.Core.Logic.EntitiesRepositories
{
    using DisneyApi.Core.Models.Context;
    using DisneyApi.Core.Models.Entities;
    using DisneyApi.Core.Models.Repository;
    using System.Threading.Tasks;

    public class UsuarioRepository : Repository<Usuario,DisneyDBContext>
    {        
        public UsuarioRepository(DisneyDBContext context): base(context)
        {

        }
               
    }
}
