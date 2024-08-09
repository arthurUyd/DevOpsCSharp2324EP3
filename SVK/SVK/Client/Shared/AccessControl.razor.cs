using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SVK.Client.Shared;

public partial class AccessControl
{
    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public SignOutSessionStateManager SignOutManager { get; set; }
    private async Task BeginSignOut(MouseEventArgs args)
    {
        await SignOutManager.SetSignOutState();
        Navigation.NavigateTo("authentication/logout");
    }
}