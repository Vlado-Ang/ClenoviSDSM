using Blazored.LocalStorage;
using ClenoviSDSM.Client.Auth;
using ClenoviSDSM.Client.Helpers;
using ClenoviSDSM.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClenoviSDSM.Client.Services
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public TokenManagerService(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync()
        {
            string token = await _localStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            if (ValidateTokenExpiration(token))
            {
                string refreshToken = await _localStorageService.GetItemAsync<string>("refreshToken");
                if (string.IsNullOrEmpty(refreshToken))
                {
                    await _localStorageService.RemoveItemAsync("token");
                    await _localStorageService.RemoveItemAsync("refreshToken");
                    return string.Empty;
                }

                TokenModel tokenModel = new TokenModel { Token = token, RefreshToken = refreshToken };
                return await RefreshTokenEndPoint(tokenModel);
            }
            else
            {
                await _localStorageService.RemoveItemAsync("token");
                await _localStorageService.RemoveItemAsync("refreshToken");
                return string.Empty;
            }


        }

        private bool ValidateTokenExpiration(string token)
        {
            List<Claim> claims = JwtParser.ParseClaimsFromJwt(token).ToList();
            if (claims?.Count == 0)
            {
                return false;
            }
            string expirationSeconds = claims.Where(_ => _.Type.ToLower() == "exp").Select(_ => _.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(expirationSeconds))
            {
                return false;
            }

            var exprationDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expirationSeconds));
            if (exprationDate < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        private async Task<string> RefreshTokenEndPoint(TokenModel tokenModel)
        {
            var response = await _httpClient.PostAsJsonAsync<TokenModel>("api/ActivateAccessTokenByRefresh", tokenModel);
            if (!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }
            AuthResponse authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            await _localStorageService.SetItemAsync<string>("token", authResponse.Token);
            await _localStorageService.SetItemAsync<string>("refreshToken", authResponse.RefreshToken);
            return authResponse.Token;
        }
    }
}
