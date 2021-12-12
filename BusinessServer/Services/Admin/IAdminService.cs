using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Services.Admin
{
    public interface IAdminService
    {
        public Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId);
        Task<bool> AddFoodGroupAsync(string foodGroupName);
        public Task<bool> AddRecipeAsync(Recipe recipe);
        public Task<bool> AddRestaurantAsync(Restaurant restaurant);
        public Task<Dictionary<int, string>> GetUnitListAsync();
        public Task<List<Restaurant>> GetRestaurantListAsync();
        public Task<List<Address>> GetAddressListAsync();
        public Task<Address> GetAddressByIdAsync(int addressId);

    }
}