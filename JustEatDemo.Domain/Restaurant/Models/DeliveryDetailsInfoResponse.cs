using JustEatDemo.Common.General;
using System.Collections.Generic;

namespace JustEatDemo.Domain.Restaurant.Models
{
    public class DeliveryDetailsInfoResponse
    {
        public List<Restaurant> Restaurants{ get; set; }
        public ErrorData Error { get; set; } = null;
    }
}
