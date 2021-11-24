// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\_Imports.razor"
using Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\Pages\Home.razor"
using Client.GroupManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\Pages\Home.razor"
using SharedLibrary.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\Pages\Home.razor"
using System.Text.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\Pages\Home.razor"
           [Authorize(Policy = "SignedIn")]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Home")]
    public partial class Home : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 17 "C:\Users\Tina Hadberg\RiderProjects\SEP3-Csharp\Client\Pages\Home.razor"
       
   // public string Name { get; set; }
    private async void CreateGroup()
    {
       
        string userAsJson = await JsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (!string.IsNullOrEmpty(userAsJson))
        {
            RegisteredUser user = JsonSerializer.Deserialize<RegisteredUser>(userAsJson);
            await GroupManager.CreateGroupAsync(user);
        }
        
        
        /*
        string serializedData = JsonSerializer.Serialize(name);
        await JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser",serializedData);
        */
      //  await GroupManager.CreateGroupAsync();
    }
    
    protected override async Task OnInitializedAsync()
    {
       /*
        var authstate = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authstate.User;
        Name = user.Identity?.Name;
        */
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JsRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private GroupManager GroupManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    }
}
#pragma warning restore 1591
