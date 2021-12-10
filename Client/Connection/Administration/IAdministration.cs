using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

namespace Client.Connection.Administration
{
    public interface IAdministration
    {
        public Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId );
        public Task<bool> AddRecipeAsync(Recipe recipe);
        public Task<bool> AddRestaurantAsync(Restaurant restaurant);
        Task<Dictionary<int, string>> GetFoodgroupListAsync();
        Task<Dictionary<int, string>>  GetUnitListAsync();
        Task<Dictionary<int, string>> GetIngredientListAsync();
        Task<List<Restaurant>> GetRestaurantListAsync();
        Task<Address> GetAddressByIdAsync(int addressId);

    }
}