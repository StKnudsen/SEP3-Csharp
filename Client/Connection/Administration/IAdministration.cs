using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;

namespace Client.Connection.Administration
{
    public interface IAdministration
    {
        public Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId );
        Task<bool> AddFoodGroup(string foodGroupName);
        public Task<bool> AddRecipeAsync(Recipe recipe);
        public Task<bool> AddRestaurantAsync(Restaurant restaurant);
        Task<Dictionary<int, string>>  GetUnitListAsync();
        Task<List<Restaurant>> GetRestaurantListAsync();
        Task<Address> GetAddressByIdAsync(int addressId);

    }
}