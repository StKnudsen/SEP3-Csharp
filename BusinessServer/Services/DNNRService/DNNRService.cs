using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Network.DNNRDataLink;

namespace BusinessServer.Services.DNNRService
{
    public class DNNRService : IDNNRService
    {
        private Dictionary<int, string> foodGroupList;
        private Dictionary<int, string> ingredientList;

        private readonly IDNNRDataLink DataLink;
        
        public DNNRService()
        {
            DataLink = new DNNRDataLink();
            foodGroupList = GetFoodgroupListAsync().Result;
            ingredientList = GetIngredientListAsync().Result;
            
        }

        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            return await DataLink.GetIngredientListAsync();
        }

        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            return await DataLink.GetFoodgroupListAsync();
        }
    }
}