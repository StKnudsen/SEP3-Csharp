using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Models;
using BusinessServer.Network.UserDataLink;
using BusinessServer.Services.Security;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDataLink _userDataLink;
        private IList<int[]> UsedGuestValues;

        public UserService()
        {
            _userDataLink = new UserDataLink();
            UsedGuestValues = new List<int[]>();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            using IHashCheck hashCheck = new Md5Check();

            string hashPassword = hashCheck.GenerateHash(password);

            RegisteredUser controlUser = await _userDataLink.GetUserAsync(username);

            return hashCheck.Compare(hashPassword, controlUser.Password);
        }

        public async Task<GuestUser> GetGuestUserAsync()
        {
            ColourAnimalCount count = await GetColourAnimalCountAsync();
            Random rnd = new Random();
            int colour, animal;
            int[] guest;

            do
            {
                colour = rnd.Next(count.ColourCount) + 1;
                animal = rnd.Next(count.AnimalCount) + 1;

                guest = new[] {colour, animal};

            } while (UsedGuestValues.Contains(guest));

            return await _userDataLink.GetGuestUserAsync(colour, animal);
        }

        public async Task<RegisteredUser> GetUserAsync(string username)
        {
            return await _userDataLink.GetUserAsync(username);
        }

        public async Task<bool> CheckUsernameAvailabilityAsync(string username)
        {
            RegisteredUser user = await _userDataLink.GetUserAsync(username);
            Console.WriteLine(user.Username is null);
            return user.Username is null;
        }

        public async Task<bool> CreateUserAsync(string username, string password)
        {
            using IHashCheck hashCheck = new Md5Check();

            var hashedPassword = hashCheck.GenerateHash(password);
            
            RegisteredUser user = new RegisteredUser
            {
                Role = "User",
                Username = username,
                Password = hashedPassword
            };

            return await _userDataLink.CreateUserAsync(user);
        }

        private async Task<ColourAnimalCount> GetColourAnimalCountAsync()
        {
            return await _userDataLink.GetColourAnimalCountAsync();
        }
    }
}