using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using ClenoviSDSM.Client.Auth;
using ClenoviSDSM.Shared.Models;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Json;

namespace ClenoviSDSM.Client.Services
{
    public class AccountService :IAcountService
    {
        private readonly AuthenticationStateProvider _customAuthenticationProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        public AccountService(ILocalStorageService localStorageService,
          AuthenticationStateProvider customAuthenticationProvider,
          HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _customAuthenticationProvider = customAuthenticationProvider;
            _httpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public async Task<bool> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<LoginModel>("api/LoginUser", model);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            AuthResponse authData = await response.Content.ReadFromJsonAsync<AuthResponse>();

            await _localStorageService.SetItemAsync("token", authData.Token);
            (_customAuthenticationProvider as CustomAuthenticationProvider).Notify();
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync("token");
            (_customAuthenticationProvider as CustomAuthenticationProvider).Notify();
            return true;
        }
    }
}
