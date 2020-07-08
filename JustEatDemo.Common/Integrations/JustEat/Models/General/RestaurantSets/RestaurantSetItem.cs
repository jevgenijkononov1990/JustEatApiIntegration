using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General.Sets
{
    public class RestaurantSetItem : ISet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<RestaurantItem> Restaurants { get; set; }
    }
}
