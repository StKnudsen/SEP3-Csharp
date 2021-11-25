using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SharedLibrary.Models;

namespace Client.Authentication
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
                    await ValidateLoginAsync(temp.Username, temp.Password);
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

            HubConnection = new HubConnectionBuilder()
                .WithUrl(uriUserhub).Build();
            await HubConnection.StartAsync();

            ClaimsIdentity identity = new ClaimsIdentity();
            bool response = await HubConnection.InvokeAsync<bool>("ValidateUserAsync", username, password);
            if (response)
            {
                RegisteredUser user = new RegisteredUser
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

        public async Task GuestLogin()
        {
            HubConnection = new HubConnectionBuilder().WithUrl(uriUserhub).Build();
            await HubConnection.StartAsync();

            ClaimsIdentity identity = new ClaimsIdentity();
            GuestUser guest = await HubConnection.InvokeAsync<GuestUser>("GetGuestUserAsync");

            identity = SetupClaimsForUser(guest);
            string serializedData = JsonSerializer.Serialize(guest);
            await JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedData);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public async Task Logout()
        {
            CachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            await JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
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