using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Server.Hubs;
using SpotMixesBlazor.Server.Services;
using SpotMixesBlazor.Server.Settings;

namespace SpotMixesBlazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Default settings
            services.AddControllersWithViews();
            services.AddRazorPages();
            // Mongo
            services.Configure<SpotMixesDatabaseSettings>(Configuration.GetSection(nameof(SpotMixesDatabaseSettings)));

            services.AddSingleton<ISpotMixesDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SpotMixesDatabaseSettings>>().Value);
            // Mail settings
            services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));
            // Injection dependencies
            services.AddSingleton<UserService>();
            services.AddSingleton<AudioService>();
            services.AddSingleton<MailService>();
            services.AddSingleton<SessionService>();
            services.AddSingleton<ReactionService>();
            services.AddSingleton<CommentService>();
            //SignalR
            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] {"application/octet-stream"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<ReactionHub>("/reactionhub");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
