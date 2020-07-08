
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using JustEatDemo.Common.Constants;
using JustEatDemo.Common.Enum;
using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations.JustEat.Models;
using JustEatDemo.Common.Integrations.JustEat.Models.General;
using JustEatDemo.Common.Integrations.JustEat.Models.Responses;
using JustEatDemo.Common.Integrations.Models;


namespace JustEatDemo.Common.Integrations
{
    public class JustEatApiIntegrationService : IJustEatApiIntegrationService
    {
        private readonly IBaseRestApiService _genericRestApiService;

        public JustEatApiIntegrationService(IBaseRestApiService genericRestApiService)
        {
            //defense

            _genericRestApiService = genericRestApiService ??
                throw new ArgumentNullException($"{GetType().Name} {CommonConstants.ConstructorInitFailure} {nameof(genericRestApiService)}");
        }

        public async Task<JustEatRestarauntResponseData> GetRestaurantListForUserByDeliveryPostcodeAsync(IJustEatRequest<JustEatRestaurantsBasicRequest> request)
        {
            if(request == null || (request.TbodyRequired == true && request.RequestBody == null))
            {
                //log 
                throw new ArgumentNullException($"{GetType().Name} {CommonConstants.ErrorMessage_CheckInputData} ");
            }

            JustEatRestarauntResponseData result = new JustEatRestarauntResponseData();

            try
            {
                string urlRequest = $"{request.CoreUrl}/{request.Endpoint}{request.Query}".ToLower();

                var apiRequestResult = await _genericRestApiService.SendApiRequestAsync<RestaurantsGeneralData, object>(urlRequest, null, request.Header, request.ApiMethod, doNotDeserializeOutput: false,
                        authRequest: new HttpAuth
                        {
                            AuthType = request.AuthType,
                            Password = request?.RequestBody?.Password,
                            Username = request?.RequestBody?.Username
                        });

                if (apiRequestResult.deserializationSuccess && apiRequestResult.responseModel != null)
                {
                    if(apiRequestResult.responseModel.Restaurants != null)
                    {
                        //returns a list of restaurants that deliver to the outcode xxxxxx, including some basic restaurant information.
                        result.Restaurants = apiRequestResult.responseModel.Restaurants.Select(x => new RestaurantShortDetails
                        {
                            RestaurantAddress  = new Address { Street = x.Address, City = x.City, Postcode = x.Postcode, AdditionalDetails = "N/A", Country = "N/A" },
                            RestaurantLocation =new Location { Latitude = x.Latitude, Longitude = x.Longitude },
                            RatingDetails  = new RatingDetails { NumberOfRatings = x.NumberOfRatings, Score = x.Score, RatingAverage = x.RatingAverage, RatingStars = x.RatingStars },
                            CuisineTypes  = x.CuisineTypes,
                            DisplayId = x.DefaultDisplayRank,
                            LogUrl = (x.Logo != null && x.Logo.Count > 0) ? x.Logo[0].StandardResolutionURL : "",
                            Id = x.Id,
                            Name = x.Name,
                            UniqueName = x.UniqueName

                        }).ToList();
                    }
                    result.Success = true;
                }
                else
                {
                    //log
                    result.Errors = new List<ErrorData>
                    {
                        new ErrorData
                        {
                            Message = "Unsuccessful context deserialization or service exception",
                            Token = ErrorTokens.ServiceError.ToString()
                        }
                    };
                }    
            }
            catch
            {
                //log  
                result.Errors = new List<ErrorData>
                    {
                        new ErrorData
                        {
                            Message = "Exception occurred during integration request execution or response handle",
                            Token = ErrorTokens.ServiceException.ToString()
                        }
                    };
            }
            return result;
        }
    }
}
