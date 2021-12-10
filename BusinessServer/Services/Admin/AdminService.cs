﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServer.Network;
using BusinessServer.Network.AdminDataLink;
using SharedLibrary.Models;

namespace BusinessServer.Services.Admin
{
    public class AdminService : IAdminService
    {
        private Dictionary<int, string> ingredientList;
        private Dictionary<int, string> foodGroupList;
        private Dictionary<int, string> unitList;
        private Dictionary<int, string> recipeList;
        private List<Restaurant> restaurantList;
        private List<Address> addressList;
        

        private readonly IAdminDataLink AdminDataLink;

        public AdminService()
        {
            AdminDataLink = new AdminDataLink();
            ingredientList = GetIngredientListAsync().Result;
            foodGroupList = GetFoodgroupListAsync().Result;
            unitList = GetUnitListAsync().Result;
            recipeList = GetRecipeListAsync().Result;
            restaurantList = GetRestaurantListAsync().Result;
            addressList = GetAddressListAsync().Result;
        }

        public async Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            if (ingredientList.ContainsValue(ingredientName.ToLower()))
            {
                throw new Exception("Ingrediens findes allerede i databasen");
            }

            await AdminDataLink.AddIngredientAsync(ingredientName, _foodGroupId);
            await GetIngredientListAsync();
            return true;
        }

        public async Task<bool> AddFoodGroup(string foodGroupName)
        {
            return await AdminDataLink.AddFoodGroup(foodGroupName);
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            if (recipeList.ContainsValue(recipe.Name.ToLower()))
            {
                throw new Exception("Opskrift findes allerede i databasen");
            }
            
            await AdminDataLink.AddRecipeAsync(recipe);
            await GetRecipeListAsync();
            return true;
        }

        public async Task<bool> AddRestaurantAsync(Restaurant restaurant)
        {
            Console.WriteLine("AdminService i AddRestaurantAsync");

            if (restaurantList.Contains(restaurant))
            {
                throw new Exception("Restaurant findes allerede i databasen");
            }
            
            //TODO: Det her check virker ikke. Pointen er, at en restaurant ikke kan oprettes på en addresse, der allerede eksisterer i databasen.
            if (addressList.Contains(restaurant.Address))
            {
                throw new Exception("Den angivne addresse er allerede brugt i databasen");
            }

            await AdminDataLink.AddRestaurantAsync(restaurant);
            await GetRestaurantListAsync();
            await GetAddressListAsync();
            return true;
        }

        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            return ingredientList = await AdminDataLink.GetIngredientListAsync();
        }

        private async Task<Dictionary<int,string>> GetRecipeListAsync()
        {
            return recipeList = await AdminDataLink.GetRecipeListAsync();
        }

        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            return foodGroupList = await AdminDataLink.GetFoodgroupListAsync();
        }

        public async Task<Dictionary<int, string>> GetUnitListAsync()
        {
            return unitList = await AdminDataLink.GetUnitListAsync();
        }

        public async Task<List<Restaurant>> GetRestaurantListAsync()
        {
            return restaurantList = await AdminDataLink.GetRestaurantListAsync();
        }   
        public async Task<List<Address>> GetAddressListAsync()
        {
            return addressList = await AdminDataLink.GetAddressListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await AdminDataLink.GetAddressByIdAsync(addressId);
        }
    }
}