using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public interface IUserService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<GuestUser> GetGuestUserAsync();
        Task<RegisteredUser> GetUserAsync(string username);
        Task<bool> CheckUsernameAvailabilityAsync(string username);
        Task<bool> CreateUserAsync(string username, string password);
    }
}