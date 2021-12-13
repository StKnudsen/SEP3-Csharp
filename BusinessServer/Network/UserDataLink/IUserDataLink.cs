using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Network.UserDataLink
{
    public interface IUserDataLink
    {
        public Task<RegisteredUser> GetUserAsync(string username);
        public Task<ColourAnimalCount> GetColourAnimalCountAsync();
        public Task<GuestUser> GetGuestUserAsync(int ColourId, int AnimalId);
        public Task<bool> CreateUserAsync(RegisteredUser user);
    }
}