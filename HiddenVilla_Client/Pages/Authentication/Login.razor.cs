using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Login
    {
        public Login()
        {
        }

        [Inject]
        IAuthenticationService authenticationService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        private AuthenticationDTO UserForAuthentication = new AuthenticationDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowAuthenticationErrors { get; set; }
        public string Errors { get; set; }
        public string ReturnUrl { get; set; }

        private async Task LoginUser()
        {
            ShowAuthenticationErrors = false;
            IsProcessing = true;
            var result = await authenticationService.Login(UserForAuthentication);

            if (result.IsAuthenticationSuccessful)
            {
                IsProcessing = false;

                var absoluteUri = new Uri(navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];

                if (String.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }
            }
            else
            {
                IsProcessing = false;
                Errors = result.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
        }
    }
}
