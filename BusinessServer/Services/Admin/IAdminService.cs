using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Services.Admin
{
    public interface IAdminService
    {
        public Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId);
        public Task<Dictionary<int, string>> GetIngredientListAsync();
        public Task<Dictionary<int, string>> GetFoodgroupListAsync();

    }
}