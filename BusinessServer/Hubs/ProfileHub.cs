using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessServer.Services.Profile;

namespace BusinessServer.Hubs
{
    public class ProfileHub
    {
        
        private readonly IProfileService ProfileService;

        public ProfileHub()
        {
            ProfileService = new ProfileService();
        }

        public async Task<Dictionary<int, string>> GetUserAllergyIngredient(int userId)
        {
            try
            {
                return await ProfileService.GetUserAllergyIngredient(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get user allergy ingredient error: {e}");
                throw;
            }
        }
        
        public async Task<Dictionary<int, string>> GetUserAllergyFoodGroup(int userId)
        {
            try
            {
                return await ProfileService.GetUserAllergyFoodGroup(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get user allergy food group error: {e}");
                throw;
            }
           
        }
    }
}