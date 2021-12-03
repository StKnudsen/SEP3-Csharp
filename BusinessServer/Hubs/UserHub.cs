﻿using System.Threading.Tasks;
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
    }
    
}