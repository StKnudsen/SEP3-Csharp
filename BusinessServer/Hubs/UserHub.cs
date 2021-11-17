using System.Threading.Tasks;
using BusinessServer.Services;
using Microsoft.AspNetCore.SignalR;

namespace BusinessServer.Hubs
{
    public class UserHub : Hub
    {
        private readonly IUserService UserService;

        public UserHub()
        {
            UserService = new UserService();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            return await UserService.ValidateUserAsync(username, password);
        }
    }
}