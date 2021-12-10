using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Services.DNNRService;
using Microsoft.AspNetCore.SignalR;

namespace BusinessServer.Hubs
{
    public class DNNRHub : Hub
    {
        private readonly IDNNRService DnnrService;

        public DNNRHub()
        {
            Console.WriteLine("#DNNRHub start");
            DnnrService = new DNNRService();
        }
        
        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            try
            {
                return await DnnrService.GetFoodgroupListAsync();
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        
        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            try
            {
                return await DnnrService.GetIngredientListAsync();
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
    }
}