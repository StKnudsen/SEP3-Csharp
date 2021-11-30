using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Services.Admin
{
    public interface IAdminService
    {
        public Task AddIngredientAsync(string ingredientName, int _foodGroupId);
        public Task GetIngredientListAsync();
        public Task GetFoodgroupListAsync();

    }
}