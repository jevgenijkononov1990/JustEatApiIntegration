using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations;
using JustEatDemo.Common.Integrations.JustEat;
using JustEatDemo.Domain.Restaurant;
using JustEatDemo.WebApp.Helpers;
using JustEatDemo.WebApp.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JustEatDemo.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            RegisterServices(services);
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IBaseRestApiService, IntegrationApiClient>();
            services.AddTransient<IJustEatApiIntegrationService, JustEatApiIntegrationService>();
            services.Configure<JustEatIntegrationSettings>(Configuration.GetSection("JustEatIntegration"));
            services.AddSingleton<IJustEatRepository, JustEatSettingsRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();


            #region Custom Middlewares

            app.UseExceptionHandling();
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }


    }
}
