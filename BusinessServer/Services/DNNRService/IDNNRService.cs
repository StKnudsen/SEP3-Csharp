using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServer.Services.DNNRService
{
    public interface IDNNRService
    {
        public Task<Dictionary<int, string>> GetIngredientListAsync();
        public Task<Dictionary<int, string>> GetFoodgroupListAsync();
    }
}