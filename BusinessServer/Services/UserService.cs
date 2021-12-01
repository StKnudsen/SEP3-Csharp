using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServer.Models;
using BusinessServer.Network;
using BusinessServer.Network.UserDataLink;
using BusinessServer.Services.Security;
using SharedLibrary.Models;

namespace BusinessServer.Services
{
    public class UserService : IUserService
    {
        private readonly IDataLink DataLink;
        private IList<int[]> UsedGuestValues;

        public UserService()
        {
            DataLink = new DataLink();
            UsedGuestValues = new List<int[]>();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            using IHashCheck hashCheck = new Md5Check();

            string hashPassword = hashCheck.GenerateHash(password);

            RegisteredUser controlUser = await DataLink.GetUserAsync(username);

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

            return await DataLink.GetGuestUserAsync(colour, animal);
        }

        private async Task<ColourAnimalCount> GetColourAnimalCountAsync()
        {
            return await DataLink.GetColourAnimalCountAsync();
        }
    }
}