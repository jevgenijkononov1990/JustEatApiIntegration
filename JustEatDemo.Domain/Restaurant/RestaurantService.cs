using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JustEatDemo.Common.Constants;
using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations;
using JustEatDemo.Common.Integrations.Enums;
using JustEatDemo.Common.Integrations.JustEat;
using JustEatDemo.Common.Integrations.JustEat.Models;
using JustEatDemo.Common.Integrations.Models;
using JustEatDemo.Domain.Restaurant.Models;


namespace JustEatDemo.Domain.Restaurant
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IJustEatApiIntegrationService _justEatIntegrationService;
        private readonly IJustEatRepository _justEatRepository;

        public RestaurantService(IJustEatRepository justEatRepository, IJustEatApiIntegrationService justEatIntegrationService)
        {
            //defense

            _justEatRepository = justEatRepository ??
                throw new ArgumentNullException($"{GetType().Name} {CommonConstants.ConstructorInitFailure} {nameof(justEatRepository)}");

            _justEatIntegrationService = justEatIntegrationService ??
                throw new ArgumentNullException($"{GetType().Name} {CommonConstants.ConstructorInitFailure} {nameof(justEatIntegrationService)}");
        }

        public async Task<DeliveryDetailsInfoResponse> GetRestaurantsListByUserAndPostCodeAsync(DeliveryDetailsInfoRequest deliveryDetails)
        {
            if (deliveryDetails == null || deliveryDetails.UserData == null)
            {
                //log
                throw new ArgumentNullException($"The ({nameof(deliveryDetails)}) object is null!");
            }

            JustEatIntegrationSettings integrationSettings = _justEatRepository.GetSettings();

            if (integrationSettings == null)
            {
                //log
                throw new ArgumentNullException($"The ({nameof(integrationSettings)}) object is null!");
            }

            string requestQuery = "";

            if (integrationSettings.QueryParameters != null && integrationSettings.QueryParameters.Count > 0)
            {
                bool firtTime = true;
                PropertyInfo[] requestPropertyInfos = typeof(DeliveryDetailsInfoRequest).GetProperties();
                foreach (var item in requestPropertyInfos)
                {
                    QueryParameter foundItem = integrationSettings.QueryParameters.SingleOrDefault(x => x.Description.ToLower() == item.Name.ToLower());

                    if (foundItem != null)
                    {
                        var objectValue = PropertyExtentions.GetPropValue<DeliveryDetailsInfoRequest>(deliveryDetails, item.Name);
                        if (firtTime)
                        {
                            requestQuery += $"?{foundItem.Key}={objectValue}";
                            firtTime = false;
                        }
                        else
                        {
                            requestQuery += $"&{foundItem.Key}={objectValue}";
                        }
                    }
                }
            }

            var result = await _justEatIntegrationService.GetRestaurantListForUserByDeliveryPostcodeAsync(new IJustEatRequest<JustEatRestaurantsBasicRequest>()
            {
                ApiMethod = HttpApiMethodsTypeConverter.StringToEnum(integrationSettings.RestaurantsRequestType),
                AuthType = HttpAuthType.BasicAuth,
                Header = integrationSettings?.RequestHeader != null ? integrationSettings.RequestHeader.Select(x => new CustomHeader { HeaderName = x.Key, HeaderValue = x.Value }).ToList() : null,
                Query = requestQuery,
                CoreUrl = integrationSettings.CoreUrl,
                Endpoint = integrationSettings.RestaurantsEndpoint,
                RequestBody = new JustEatRestaurantsBasicRequest
                {
                    Username = deliveryDetails.UserData.UserName,
                    Password = deliveryDetails.UserData.Password,
                    Postcode = deliveryDetails.Postcode
                },
                TbodyRequired = true
            });

            if (result == null)
            {
                //log
                throw new ArgumentNullException($"Unpredicted error occured in {nameof(_justEatIntegrationService)} response");
            }

            return new DeliveryDetailsInfoResponse
            {
                Restaurants = result.Restaurants != null ? result.Restaurants.Select(x => new Models.Restaurant
                {
                    Id = x.DisplayId,
                    CuisineTypes = (x.CuisineTypes !=null) ? x.CuisineTypes.Select(y => y.Name).ToList() : null,
                    Rating = (x.RatingDetails != null ) ? x.RatingDetails.RatingAverage: 0,
                    ImageUrl = x.LogUrl,
                    Name = x.Name
                }).OrderBy(v => v.Id).ToList() : null,

                Error = (result.Errors != null && result.Errors.Count > 0) ? result.Errors[0] : null,
            };
        }
    }
}
