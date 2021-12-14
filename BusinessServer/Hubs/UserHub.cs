using System;
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Services;
using Microsoft.AspNetCore.SignalR;
using SharedLibrary.Models;

namespace BusinessServer.Hubs
{
    public class UserHub : Hub
    {
        private readonly IUserService UserService;

        public UserHub()
        {
            UserService = new UserService();
        }

        public async Task<bool> CreateUserAsync(string username, string password)
        {
            return await UserService.CreateUserAsync(username, password);
        }

        public async Task<bool> CheckUsernameAvailabilityAsync(string username)
        {
            Console.WriteLine("UserHUB");
            return await UserService.CheckUsernameAvailabilityAsync(username);
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            return await UserService.ValidateUserAsync(username, password);
        }

        public async Task<GuestUser> GetGuestUserAsync()
        {
            return await UserService.GetGuestUserAsync();
        }

        public async Task<RegisteredUser> GetUserAsync(string username)
        {
            return await UserService.GetUserAsync(username);
        }
        
        //  For allergy registration
        public async Task<Dictionary<int, string>> GetAllergyFoodGroupListAsync(int userId)
        {
            return await UserService.getAllergyFoodGroupListAsync(userId);
        }

        public async Task<Dictionary<int, string>> GetAllergyIngredientListAsync(int userId)
        {
            return await UserService.getAllergyIngredientListAsync(userId);
        }

        public async Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId)
        {
            return await UserService.SetUserAllergyFoodGroupAsync(userId, foodGroupId);
        }

        public async Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredient)
        {
            return await UserService.SetUserAllergyIngredientAsync(userId, ingredient);
        }
    }
    
}