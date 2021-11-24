using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public interface IUserService
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<GuestUser> GetGuestUserAsync();
    }
}