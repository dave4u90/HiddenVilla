using System;
using System.Threading.Tasks;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Logout
    {
        public Logout()
        {
        }

        [Inject]
        IAuthenticationService authenticationService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await authenticationService.Logout();
            navigationManager.NavigateTo("/");
        }
    }
}
