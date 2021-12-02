using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Services.Admin
{
    public interface IAdminService
    {
        public Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId);
        public Task<bool> AddRecipeAsync(Recipe recipe);
        public Task<Dictionary<int, string>> GetIngredientListAsync();
        public Task<Dictionary<int, string>> GetFoodgroupListAsync();
        public Task<Dictionary<int, string>> GetUnitListAsync();
    }
}