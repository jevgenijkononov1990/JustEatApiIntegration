using Moq;
using System;
using JustEatDemo.Common.Integrations;
using JustEatDemo.Common.Integrations.JustEat.Models;
using JustEatDemo.Common.Integrations.JustEat.Models.Responses;


namespace JustEatDemo.Tests.Mocks
{
    public class JustEatApiIntegrationServiceMock : Mock<IJustEatApiIntegrationService>
    {
        public JustEatApiIntegrationServiceMock Mock_GetRestaurantDetailsByUserAndPostcode_CustomReturn( JustEatRestarauntResponseData response)
        {
            Setup(x => x.GetRestaurantListForUserByDeliveryPostcodeAsync(It.IsAny<IJustEatRequest<JustEatRestaurantsBasicRequest>>())).ReturnsAsync(response);
            return this;
        }

        public JustEatApiIntegrationServiceMock Mock_GetRestaurantDetailsByUserAndPostcode_ThrowsException()
        {
            Setup(x => x.GetRestaurantListForUserByDeliveryPostcodeAsync(It.IsAny<IJustEatRequest<JustEatRestaurantsBasicRequest>>())).ThrowsAsync(new Exception());
            return this;
        }
    }
}
