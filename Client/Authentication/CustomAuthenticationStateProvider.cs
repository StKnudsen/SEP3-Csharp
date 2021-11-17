using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;

namespace Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime JsRuntime;
        private HubConnection HubConnection;
        private User CachedUser;

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
                    User temp = JsonSerializer.Deserialize<User>(userAsJson);
                    ValidateLoginAsync(temp.Username, temp.Password);
                }
            }
            else
            {
                identity = SetupClaimsForUser(CachedUser);
            }

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async void ValidateLoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Enter username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Enter password");
            }

            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/userhub").Build();
            await HubConnection.StartAsync();

            ClaimsIdentity identity = new ClaimsIdentity();
            if (await HubConnection.InvokeAsync<bool>("ValidateUserAsync", username, password))
            {
                User user = new User
                {
                    Username = username,
                    Password = password
                };

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

        private ClaimsIdentity SetupClaimsForUser(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim("SignedIn", "true"));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
    }
}