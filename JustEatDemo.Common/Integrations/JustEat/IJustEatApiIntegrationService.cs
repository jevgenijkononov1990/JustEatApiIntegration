using JustEatDemo.Common.Integrations.JustEat.Models;
using JustEatDemo.Common.Integrations.JustEat.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustEatDemo.Common.Integrations
{
    public interface IJustEatApiIntegrationService
    {
        Task<JustEatRestarauntResponseData> GetRestaurantListForUserByDeliveryPostcodeAsync(IJustEatRequest<JustEatRestaurantsBasicRequest> request);
    }
}
