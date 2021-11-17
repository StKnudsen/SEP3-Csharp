using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Network
{
    public interface IDataLink
    {
        public Task<User> GetUserAsync(string username);
    }
}