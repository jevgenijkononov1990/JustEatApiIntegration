using System;
using System.Net;
using System.Threading.Tasks;
using JustEatDemo.Common.Constants;
using JustEatDemo.Common.Enum;
using JustEatDemo.Common.General;
using JustEatDemo.Domain.CommonLogic;
using JustEatDemo.Domain.Restaurant;
using JustEatDemo.Domain.Restaurant.Models;
using JustEatDemo.WebApp.Responses;
using Microsoft.AspNetCore.Mvc;


namespace JustEatDemo.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        public readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            //defense
            _restaurantService = restaurantService ?? throw new ArgumentNullException($"{this.GetType().Name} {CommonConstants.ConstructorInitFailure} {nameof(restaurantService)}");
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurantsDeliveryDataByPostCode(string username, string password, string postcode)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(postcode))
                {
                    //log
                    return BadRequest(new ResponseWrapper<object>
                    {
                        Result = null,
                        Error = new ErrorData
                        {
                            Message = CommonConstants.ErrorMessage_CheckInputData,
                            Token = ErrorTokens.WrongInput.ToString()
                        }
                    });
                }

                DeliveryDetailsInfoResponse result = await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(new DeliveryDetailsInfoRequest
                {
                    Postcode = postcode,
                    UserData = new UserCredentials
                    {
                        UserName = username,
                        Password = password
                    }
                });

                if(result != null)
                {
                    return Ok(new ResponseWrapper<DeliveryDetailsResponse>
                    {
                        Result = new DeliveryDetailsResponse
                        {
                            Restaurants = result.Restaurants
                        },
                        Error = result.Error
                    });
                }
                else
                {
                    //log
                    return BadRequest(new ResponseWrapper<object>
                    {
                        Result = null,
                        Error = new ErrorData
                        {
                            Message = CommonConstants.ErrorMessage_UnsuccessfulFinish,
                            Token = ErrorTokens.NullResponse.ToString()
                        }
                    });
                }
            }
            catch(Exception)
            {
                // log ex 
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}