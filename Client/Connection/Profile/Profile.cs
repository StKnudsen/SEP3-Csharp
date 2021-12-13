using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Connection.Profile
{
    public class Profile : IProfile
    {
        private readonly string uriProfileHub = "https://localhost:5001/profileHub";
        private HubConnection HubConnection;

        public Profile() { }
        public Task<Dictionary<int, string>> GetUserAllergyFoodGroup(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Dictionary<int, string>> GetUserAllergyIngredient(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}