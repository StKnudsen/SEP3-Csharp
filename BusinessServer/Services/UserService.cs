using System;
using System.Threading.Tasks;
using BusinessServer.Network;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public class UserService : IUserService
    {
        private readonly IDataLink DataLink;

        public UserService()
        {
            DataLink = new DataLink();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            using IHashCheck hashCheck = new Md5Check();

            string hashPassword = hashCheck.GenerateHash(password);

            User controlUser = await DataLink.GetUserAsync(username);

            return hashCheck.Compare(hashPassword, controlUser.Password);
        }
    }
}