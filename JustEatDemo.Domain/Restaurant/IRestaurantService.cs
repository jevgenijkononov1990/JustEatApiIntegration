using JustEatDemo.Domain.Restaurant.Models;
using System.Threading.Tasks;

namespace JustEatDemo.Domain.Restaurant
{
    public interface IRestaurantService
    {
        Task<DeliveryDetailsInfoResponse> GetRestaurantsListByUserAndPostCodeAsync(DeliveryDetailsInfoRequest deliveryDetails);
    }
}
