using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Network.AdminDataLink
{
    public interface IAdminDataLink
    {
        Task<bool> AddIngredientAsync(string ingredientName, int foodGroupId);
        Task<bool> AddRecipeAsync(Recipe recipe);
        Task<bool> AddRestaurantAsync(Restaurant restaurant);
        Task<Dictionary<int, string>>  GetUnitListAsync();
        Task<Dictionary<int, string>> GetRecipeListAsync();
        Task<List<Restaurant>> GetRestaurantListAsync();
        Task<List<Address>> GetAddressListAsync();
        Task<Address> GetAddressByIdAsync(int addressId);
    }
}