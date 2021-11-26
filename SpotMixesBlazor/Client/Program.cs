using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using SpotMixesBlazor.Client.Authorization;
using SpotMixesBlazor.Client.Helpers;

namespace SpotMixesBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("kiul");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);
            
            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            
            // Authentication provider JWT
            services.AddScoped<AuthenticationProviderJwt>();
            
            services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJwt>(
                provider => provider.GetRequiredService<AuthenticationProviderJwt>());
            
            services.AddScoped<ILoginService, AuthenticationProviderJwt>(
                provider => provider.GetRequiredService<AuthenticationProviderJwt>());
            
            // Pagination services
            services.AddSingleton<AudioPagingService>();
            services.AddSingleton<DjPagingService>();
        }
    }
}
