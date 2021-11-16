using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace SpotMixesBlazor.Client.Authorization
{
    public class AuthenticationProviderJwt : AuthenticationStateProvider, ILoginService
    {
        private const string TokenKey = "JwtToken";
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        private AuthenticationState Anonymous =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public AuthenticationProviderJwt(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);

            if (string.IsNullOrEmpty(token))
            {
                return Anonymous;
            }

            return BuildAuthenticationState(token);
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("bearer", token);
            
            return new AuthenticationState(new ClaimsPrincipal(
                new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }
        
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

                if (roles != null)
                {
                    if (roles.ToString().Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                    }

                    keyValuePairs.Remove(ClaimTypes.Role);
                }

                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            }

            return claims;
        }
        
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(string token)
        {
            await _jsRuntime.InvokeAsync<string>("localStorage.setItem", TokenKey, token);
            var authenticationState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", TokenKey);
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", "Email");
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", "DisplayName");
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", "IsDj");
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", "UrlProfilePicture");
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", "UrlProfile");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }
    }
}