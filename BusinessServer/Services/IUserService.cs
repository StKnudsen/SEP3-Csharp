using System.Threading.Tasks;

namespace BusinessServer.Services
{
    public interface IUserService
    {
        Task<bool> ValidateUserAsync(string username, string password);
    }
}