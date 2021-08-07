namespace DisneyApi.Core.Api.Configuration
{
    using DisneyApi.Core.Models.Entities;
    using System.Threading.Tasks;
    public interface IMailService
    {
        Task SendEmialAsync(User user);
    }
}
