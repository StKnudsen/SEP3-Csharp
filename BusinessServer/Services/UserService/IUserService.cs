using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Models;
using SharedLibrary.Models;
﻿using System.Threading.Tasks;
using BusinessServer.Services.GroupService;
using SharedLibrary.Models.User;

namespace BusinessServer.Services.UserService
{
    public interface IUserService: IGetUserInfo
    {
        Task<bool> ValidateUserAsync(string username, string password);
        Task<GuestUser> GetGuestUserAsync();
        
        Task<bool> CheckUsernameAvailabilityAsync(string username);
        Task<bool> CreateUserAsync(string username, string password);
        
        //  For allergy registration
    //    Task<Dictionary<int, string>> getAllergyFoodGroupListAsync(int userId);
      //  Task<Dictionary<int, string>> getAllergyIngredientListAsync(int userId);
        Task<bool> SetUserAllergyFoodGroupAsync(int userId, int foodGroupId);
        Task<bool> SetUserAllergyIngredientAsync(int userId, int ingredient);
    }
}