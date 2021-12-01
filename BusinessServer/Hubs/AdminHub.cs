using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Services.Admin;
using Microsoft.AspNetCore.SignalR;

namespace BusinessServer.Hubs
{
    public class AdminHub : Hub
    {

        private readonly IAdminService AdminService;

        public AdminHub(IAdminService adminService)
        {
            AdminService = adminService;
        }

        public async Task AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            await AdminService.AddIngredientAsync(ingredientName, _foodGroupId);
        }

        public async Task<Dictionary<int, string>> getFoodgroupListAsync()
        {
           return await AdminService.GetFoodgroupListAsync();
        }
    }
}