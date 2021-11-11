using System.Threading.Tasks;
using BusinessServer.Models;

namespace BusinessServer.Network
{
    public interface IDataLink
    {
        public Task<bool> ValidateUser(User user);
    }
}