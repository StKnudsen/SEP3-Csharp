using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Network
{
    public interface IDataLink
    {
        public Task<bool> ValidateUser(User user);
    }
}