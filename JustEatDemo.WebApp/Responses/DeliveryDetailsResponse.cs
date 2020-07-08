using JustEatDemo.Domain.Restaurant.Models;
using System.Collections.Generic;

namespace JustEatDemo.WebApp.Responses
{
    public class DeliveryDetailsResponse
    {
        public List<Restaurant> Restaurants { get; set; }
    }
}
