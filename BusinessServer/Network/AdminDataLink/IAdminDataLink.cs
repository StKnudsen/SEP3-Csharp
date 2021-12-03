using System.Collections.Generic;
using System.Threading.Tasks;
using SharedLibrary.Models;

namespace BusinessServer.Network.AdminDataLink
{
    public interface IAdminDataLink
    {
        Task<bool> AddIngredientAsync(string ingredientName, int foodGroupId);
        Task<bool> AddRecipeAsync(Recipe recipe);
        Task<Dictionary<int, string>> getFoodgroupList();
        Task<Dictionary<int, string>> GetIngredientListAsync();
        Task<Dictionary<int, string>>  GetUnitListAsync();
        Task<Dictionary<int, string>> GetRecipeListAsync();
    }
}