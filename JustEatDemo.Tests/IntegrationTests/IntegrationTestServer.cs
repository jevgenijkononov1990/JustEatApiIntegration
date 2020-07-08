using JustEatDemo.Domain.Restaurant;
using System;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using JustEatDemo.WebApp;

namespace JustEatDemo.Tests.IntegrationTests
{
    public class IntegrationTestServer
    {
        public readonly IRestaurantService _restaurantService;

        public IntegrationTestServer(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
        }


        public HttpClient CreateClient() =>
            Initialization().CreateClient();

        private TestServer Initialization()
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder();
            webHostBuilder.UseStartup<Startup>().
                ConfigureTestServices(services =>
                {
                    services.AddSingleton(_restaurantService);
                });
            TestServer testServer = new TestServer(webHostBuilder);

            return testServer;
        }
    }
}
