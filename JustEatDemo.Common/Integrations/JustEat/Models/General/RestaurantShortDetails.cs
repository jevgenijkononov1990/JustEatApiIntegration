using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations.JustEat.Models.General.Restaurant;
using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General
{
    public class RestaurantShortDetails : ICommonData
    {
        public Address RestaurantAddress { get; set; }
        public Location RestaurantLocation { get; set; }
        public RatingDetails RatingDetails { get; set; }
        public List<CuisineTypeItem> CuisineTypes { get; set; }
        public DeliveryDetails DeliveryOptions { get; set; }
        public long DisplayId { get; set; }
        public string LogUrl { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string UniqueName { get; set; }
    }
}
