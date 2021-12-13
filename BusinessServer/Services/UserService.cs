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
        private readonly IUserDataLink UserDataLink;
        private IList<int[]> UsedGuestValues;

        public UserService()
        {
            UserDataLink = new UserDataLink();
            UsedGuestValues = new List<int[]>();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            using IHashCheck hashCheck = new Md5Check();

            string hashPassword = hashCheck.GenerateHash(password);

            RegisteredUser controlUser = await UserDataLink.GetUserAsync(username);

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

            return await UserDataLink.GetGuestUserAsync(colour, animal);
        }

        public async Task<RegisteredUser> GetUserAsync(string username)
        {
            return await UserDataLink.GetUserAsync(username);
        }

        public async Task<Dictionary<int, string>> getAllergyFoodGroupListAsync(int userId)
        {
            return await UserDataLink.getAllergyFoodGroupListAsync(userId);
        }

        public async Task<Dictionary<int, string>> getAllergyIngredientListAsync(int userId)
        {
            return await UserDataLink.getAllergyIngredientListAsync(userId);
        }

        public async Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId)
        {
            return await UserDataLink.SetUserAllergyFoodGroupAsync(userId, foodGroupId);
        }

        public async Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredient)
        {
            return await UserDataLink.SetUserAllergyIngredientAsync(userId, ingredient);
        }

        private async Task<ColourAnimalCount> GetColourAnimalCountAsync()
        {
            return await UserDataLink.GetColourAnimalCountAsync();
        }
    }
}