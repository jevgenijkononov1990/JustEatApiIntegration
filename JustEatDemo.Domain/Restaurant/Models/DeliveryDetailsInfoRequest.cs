using JustEatDemo.Domain.CommonLogic;

namespace JustEatDemo.Domain.Restaurant.Models
{
    public class DeliveryDetailsInfoRequest
    {
        public string Postcode { get; set; }
        public UserCredentials UserData { get; set; }
    }
}
