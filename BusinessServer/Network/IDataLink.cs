using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;

namespace BusinessServer.Network
{
    public interface IDataLink
    {
        public Task<RegisteredUser> GetUserAsync(string username);
        public Task<ColourAnimalCount> GetColourAnimalCountAsync();
        public Task<GuestUser> GetGuestUserAsync(int ColourId, int AnimalId);
    }
}