using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

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

        public async Task<bool> AddFoodGroupAsync(string foodGroupName)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                    await HubConnection.StartAsync();
                }

                return await HubConnection.InvokeAsync<bool>("AddFoodGroupAsync", foodGroupName);
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

            return await HubConnection.InvokeAsync<Address>("GetAddressByIdAsync", addressId);
        }
//TODO: Den her metode skal laves, så en liste med alle registerede brugere kan vises på admin-siden, når admin vil tilføje en restaurant. Her skal nemlig kobles en restaurantejer til.
        public async Task<Dictionary<int, string>> GetUsersAndRestaurateurListAsync()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriAdminhub).Build();
                await HubConnection.StartAsync();
            }

            return await HubConnection.InvokeAsync<Dictionary<int, string>>("GetUsersAndRestaurateurListAsync");
        }
    }
}