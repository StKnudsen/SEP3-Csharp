using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Services.Admin;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;

namespace BusinessServer.Hubs
{
    public class AdminHub : Hub
    {
        private readonly IAdminService AdminService;

        public AdminHub()
        {
            AdminService = new AdminService();
        }

        public async Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            try
            {
                return await AdminService.AddIngredientAsync(ingredientName, _foodGroupId);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<bool> AddFoodGroup(string foodGroupName)
        {
            return await AdminService.AddFoodGroup(foodGroupName);
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            try
            {
                return await AdminService.AddRecipeAsync(recipe);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }  
        public async Task<bool> AddRestaurantAsync(Restaurant restaurant)
        {
            try
            {
                Console.WriteLine("AdminHub i AddRestaurantAsync");
                return await AdminService.AddRestaurantAsync(restaurant);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            try
            {
                return await AdminService.GetFoodgroupListAsync();
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
                return await AdminService.GetIngredientListAsync();
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<Dictionary<int, string>> GetUnitListAsync()
        {
            try
            {
                return await AdminService.GetUnitListAsync();
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<List<Restaurant>> GetRestaurantListAsync()
        {
            try
            {
                return await AdminService.GetRestaurantListAsync();
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        } 
        
        public async Task<List<Address>> GetAddressListAsync()
        {
            try
            {
                return await AdminService.GetAddressListAsync();
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            try
            {
                return await AdminService.GetAddressByIdAsync(addressId);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

    }
}