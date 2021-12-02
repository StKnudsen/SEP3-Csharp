using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;

namespace Client.Connection.Administration
{
    public interface IAdministration
    {
        public Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId );
        public Task<bool> AddRecipeAsync(Recipe recipe);
        Task<Dictionary<int, string>> GetFoodgroupListAsync();
        Task<Dictionary<int, string>>  GetUnitListAsync();
        Task<Dictionary<int, string>> GetIngredientListAsync();
    }
}