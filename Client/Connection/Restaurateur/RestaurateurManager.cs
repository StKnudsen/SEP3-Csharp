using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;
using SharedLibrary.Models.Restaurateur;

namespace Client.Connection.Restaurateur
{
    public class RestaurateurManager : IRestaurateurManager
    {
        private readonly IJSRuntime JsRuntime;
        private readonly string uriRestauranthub = "https://localhost:5001/restauranthub";
        private HubConnection HubConnection;

        public RestaurateurManager(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }
        
        public async Task<bool> AddDishAsync(Dish dish)
        {
            try
            {
                Console.WriteLine("Nåede til RestaurateurManager");
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriRestauranthub).Build();
                    await HubConnection.StartAsync();
                }

                return await HubConnection.InvokeAsync<bool>
                    ("AddDishAsync", dish);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<List<Dish>> GetDishListAsync(int restaurantId)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriRestauranthub).Build();
                    await HubConnection.StartAsync();
                }

                return await HubConnection.InvokeAsync<List<Dish>>("GetDishListAsync", restaurantId);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        public async Task<List<Restaurant>> GetRestaurantsFromRestaurateurIdAsync(int restaurateurId)
        {
            try
            {
                if (HubConnection is null)
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(uriRestauranthub).Build();
                    await HubConnection.StartAsync();
                }

                return await HubConnection.InvokeAsync<List<Restaurant>>("GetRestaurantsFromRestaurateurIdAsync",
                    restaurateurId);
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
    }
}