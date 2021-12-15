using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Network.DNNRDataLink;

namespace BusinessServer.Services.DNNRService
{
    public class DNNRService : IDNNRService
    {
        private readonly IDNNRDataLink DataLink;
        
        public DNNRService()
        {
            DataLink = new DNNRDataLink();
        }

        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            return await DataLink.GetIngredientListAsync();
        }

        public async Task<Dictionary<int, string>> GetFoodGroupListAsync()
        {
            return await DataLink.GetFoodgroupListAsync();
        }
    }
}