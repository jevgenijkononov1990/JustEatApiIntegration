using Moq;
using System;
using JustEatDemo.Domain.Restaurant;
using JustEatDemo.Domain.Restaurant.Models;

namespace JustEatDemo.Tests.IntegrationTests.RestaurantController
{
    public class RestaurantServiceMock : Mock<IRestaurantService>
    {
        public RestaurantServiceMock Mock_GetRestaurantsListByUserAndPostCodeAsyns_CustomReturn(DeliveryDetailsInfoResponse deliveryDetailsInfoResponse)
        {
            Setup(x => x.GetRestaurantsListByUserAndPostCodeAsync(It.IsAny<DeliveryDetailsInfoRequest>()))
                .ReturnsAsync(deliveryDetailsInfoResponse);
            return this;
        }

        public RestaurantServiceMock Mock_GetRestaurantsListByUserAndPostCodeAsyn_ThrowsException()
        {
            Setup(x => x.GetRestaurantsListByUserAndPostCodeAsync(It.IsAny<DeliveryDetailsInfoRequest>()))
                .ThrowsAsync(new Exception("Exception"));

            return this;

        }
    }
}
