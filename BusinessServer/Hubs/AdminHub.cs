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
            Console.WriteLine("AdminHub er nået til AddRecipeAsync " + recipe.RecipeIngredient.Count);

            try
            {
                return await AdminService.AddRecipeAsync(recipe);
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
    }
}