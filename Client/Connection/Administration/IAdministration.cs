using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using SharedLibrary.Models.Recipe;
using SharedLibrary.Models.Restaurateur;

namespace Client.Connection.Administration
{
    public interface IAdministration
    {
        Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId );
        Task<bool> AddFoodGroupAsync(string foodGroupName);
        Task<bool> AddRecipeAsync(Recipe recipe);
        Task<bool> AddRestaurantAsync(Restaurant restaurant);
        Task<Dictionary<int, string>>  GetUnitListAsync();
        Task<List<Restaurant>> GetRestaurantListAsync();
        Task<Address> GetAddressByIdAsync(int addressId);
        Task<Dictionary<int,string>> GetUsersAndRestaurateurListAsync();
    }
}