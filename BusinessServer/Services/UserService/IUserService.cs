using System.Threading.Tasks;
using SharedLibrary.Models.User;

namespace BusinessServer.Services.UserService
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