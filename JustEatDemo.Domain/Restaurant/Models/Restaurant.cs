using System.Collections.Generic;

namespace JustEatDemo.Domain.Restaurant.Models
{
    public class Restaurant : IRestaurant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<string> CuisineTypes { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
    }
}
