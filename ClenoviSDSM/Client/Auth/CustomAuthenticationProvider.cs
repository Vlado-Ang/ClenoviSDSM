using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using ClenoviSDSM.Client.Helpers;
using ClenoviSDSM.Client.Services;


// https://www.learmoreseekmore.com/2020/10/blazor-webassembly-custom-authentication-from-scratch.html

namespace ClenoviSDSM.Client.Auth
{
    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ITokenManagerService _tokenManagerService;

        public CustomAuthenticationProvider(ITokenManagerService tokenManagerService, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _tokenManagerService = tokenManagerService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //string token = await _localStorageService.GetItemAsync<string>("token");
            string token = await _tokenManagerService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity() { }));
                return anonymous;
            }
            var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var loginUser = new AuthenticationState(userClaimPrincipal);
            return loginUser;
        }

        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
