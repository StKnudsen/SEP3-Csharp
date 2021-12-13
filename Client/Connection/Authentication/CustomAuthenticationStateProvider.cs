using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;

namespace Client.Connection.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime JsRuntime;
        private readonly string uriUserhub = "https://localhost:5001/userhub";
        private HubConnection HubConnection;
        public User CachedUser { get; set; }

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            if (CachedUser is null)
            {
                string userAsJson = await JsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    RegisteredUser temp = JsonSerializer.Deserialize<RegisteredUser>(userAsJson);

                    if (temp.Password is not null)
                    {
                        await ValidateLoginAsync(temp.Username, temp.Password);
                    }
                }
            }
            else
            {
                identity = SetupClaimsForUser(CachedUser);
            }

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task ValidateLoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Enter username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Enter password");
            }

            await Connect();

            ClaimsIdentity identity = new ClaimsIdentity();
            bool response = await HubConnection.InvokeAsync<bool>("ValidateUserAsync", username, password);
            if (response)
            {
               RegisteredUser user = await HubConnection.InvokeAsync<RegisteredUser>("GetUserAsync", username);

               identity = SetupClaimsForUser(user);
                string serializedData = JsonSerializer.Serialize(user);
                await JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedData);
                CachedUser = user;
            }
            else
            {
                throw new Exception("User not valid");
            }
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public async Task GuestLoginAsync()
        {
            await Connect();

            ClaimsIdentity identity = new ClaimsIdentity();
            GuestUser guest = await HubConnection.InvokeAsync<GuestUser>("GetGuestUserAsync");

            identity = SetupClaimsForUser(guest);
            string serializedData = JsonSerializer.Serialize(guest);
            await JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedData);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public async Task<bool> CheckUsernameAvailabilityAsync(string username)
        {
            await Connect();

            return await HubConnection.InvokeAsync<bool>("CheckUsernameAvailabilityAsync", username);
        }

        private async Task Connect()
        {
            if (HubConnection is null)
            {
                HubConnection = new HubConnectionBuilder().WithUrl(uriUserhub).Build();
                await HubConnection.StartAsync();
            }
        }

        public async Task<bool> CreateUserAsync(string username, string password)
        {
            await Connect();
            return await HubConnection.InvokeAsync<bool>("CreateUserAsync", username, password);
        }

        public async Task LogoutAsync()
        {
            CachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            await JsRuntime.InvokeVoidAsync("sessionStorage.clear");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity SetupClaimsForUser(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim("SignedIn", "true"));
            claims.Add(new Claim( "Role",user.Role));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }

    }
}