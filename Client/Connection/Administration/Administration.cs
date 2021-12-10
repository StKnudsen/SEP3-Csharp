using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;

namespace Client.Connection.Administration
{
    public class Administration : IAdministration
    {
        private readonly IJSRuntime JsRuntime;
        private readonly string uriAdminhub = "https://localhost:5001/adminhub";
        private HubConnection HubConnection;

        public Administration(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public async Task<bool> AddIngredientAsync(string ingredientName, int _foodGroupId)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                    await HubConnection.StartAsync();
                }
                
                return  await HubConnection.InvokeAsync<bool>
                    ("AddIngredientAsync", ingredientName, _foodGroupId);
            }
            catch (Exception e)
            {
                throw(new Exception(e.Message));
            }
        }

        public async Task<bool> AddFoodGroup(string foodGroupName)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                    await HubConnection.StartAsync();
                }

                return await HubConnection.InvokeAsync<bool>("AddFoodGroup", foodGroupName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                    await HubConnection.StartAsync();
                }

                Console.WriteLine("Administration: " + recipe.RecipeIngredient.Count);
                return await HubConnection.InvokeAsync<bool>("AddRecipeAsync", recipe);
            }
            catch (Exception e)
            {
                throw(new Exception(e.Message));
            }
        } 
        public async Task<bool> AddRestaurantAsync(Restaurant restaurant)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                    await HubConnection.StartAsync();
                }
                
                Console.WriteLine("Administration added restaurant: " + restaurant.Name);
                return await HubConnection.InvokeAsync<bool>("AddRestaurantAsync", restaurant);
            }
            catch (Exception e)
            {
                throw(new Exception(e.Message));
            }
        }

        public async Task<Dictionary<int, string>> GetFoodgroupListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Dictionary<int, string>>
                ("GetFoodgroupListAsync");
        }

        public async Task<Dictionary<int, string>> GetUnitListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Dictionary<int, string>>
                ("GetUnitListAsync");
        }

        public async Task<Dictionary<int, string>> GetIngredientListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Dictionary<int, string>>
                ("GetIngredientListAsync");
        }

        public async Task<List<Restaurant>> GetRestaurantListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<List<Restaurant>>("GetRestaurantListAsync");
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Address>("GetAddressByIdAsync");
        }
    }
}