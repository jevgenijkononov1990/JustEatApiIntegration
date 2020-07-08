using JustEatDemo.Common.Integrations;
using JustEatDemo.Common.Integrations.JustEat.Models;
using JustEatDemo.Common.Integrations.JustEat.Models.General;
using JustEatDemo.Common.Integrations.JustEat.Models.General.Restaurant;
using JustEatDemo.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JustEatDemo.Tests.ServiceTests
{
    public class JustEatApiIntegrationServiceTests
    {
        private BaseRestApiServiceMock _baseRestApiServiceMock;
        private JustEatApiIntegrationService _justEatApiIntegrationService;


        public JustEatApiIntegrationServiceTests()
        {
            _baseRestApiServiceMock = new BaseRestApiServiceMock();
            _justEatApiIntegrationService = new JustEatApiIntegrationService(_baseRestApiServiceMock.Object);
        }

        [Fact]
        public void Test_JustEatApiIntegrationService_Constructor_ForDefense_When_Dependency_Null_Result_ThrowException()
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {

                new JustEatApiIntegrationService(null);

            });

            //Arrange
            Assert.Throws<ArgumentNullException>(arrange);
        }

        [Fact]
        public async Task Test_GetRestaurantListForUserByDeliveryPostcodeAsync_WhenInputData_Null_ThrowArgumentNullException()
        {
            //Arrange

            //Act
            Func<Task> act = async () => await _justEatApiIntegrationService.GetRestaurantListForUserByDeliveryPostcodeAsync(null);

            //Arrange
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }


        [Fact]
        public async Task Test_GetRestaurantListForUserByDeliveryPostcodeAsync_WhenTbodyRequiredTrue_ThrowArgumentNullException()
        {
            //Arrange
            var input = new IJustEatRequest<JustEatRestaurantsBasicRequest> { TbodyRequired = true, RequestBody = null };

            //Act
            Func <Task> act = async () => await _justEatApiIntegrationService.GetRestaurantListForUserByDeliveryPostcodeAsync(input);

            //Arrange
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task Test_GetRestaurantListForUserByDeliveryPostcodeAsync_WhenBaseRestService_ThrowsException_ResultResponseNotEmptyContainsErrors()
        {
            //Arrange
            var input = new IJustEatRequest<JustEatRestaurantsBasicRequest> { TbodyRequired = false, RequestBody = null };
            _baseRestApiServiceMock.Mock_SendApiRequestAsync_SkipTClassInit_ThrowsException();

            //Act
            var result = await _justEatApiIntegrationService.GetRestaurantListForUserByDeliveryPostcodeAsync(input);

            //Arrange
            //Arrange
            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Restaurants);
            Assert.False(result.Success);
            Assert.True(result.Errors.Count > 0);
        }

        [Fact]
        public async Task Test_GetRestaurantListForUserByDeliveryPostcodeAsync_WhenJsonDeserializationNoSuccess()
        {
            //Arrange
            var input = new IJustEatRequest<JustEatRestaurantsBasicRequest> { TbodyRequired = false, RequestBody = null };
            _baseRestApiServiceMock.Mock_SendApiRequestAsync_With_Custom_Response<object,object>(null, null, false);

            //Act
            var result = await _justEatApiIntegrationService.GetRestaurantListForUserByDeliveryPostcodeAsync(input);

            //Arrange
            Assert.NotNull(result);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Restaurants);
            Assert.False(result.Success);
            Assert.True(result.Errors.Count > 0);
        }
        [Fact]
        public async Task Test_GetRestaurantListForUserByDeliveryPostcodeAsync_WhenJsonDeserializationSuccessAndResponseIsValid()
        {
            //Arrange
            var input = new IJustEatRequest<JustEatRestaurantsBasicRequest> { TbodyRequired = false, RequestBody = null };
            _baseRestApiServiceMock
                .Mock_SendApiRequestAsync_With_Custom_Response<RestaurantsGeneralData, object>(new RestaurantsGeneralData { }, null, true );

            //Act
            var result = await _justEatApiIntegrationService.GetRestaurantListForUserByDeliveryPostcodeAsync(input);

            //Arrange
            Assert.NotNull(result);
            Assert.Null(result.Restaurants);
            Assert.Null(result.Errors);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Test_GetRestaurantListForUserByDeliveryPostcodeAsync_WhenJsonDeserializationSuccess_AndRestaurant_ArrayNotNUll_AndResponseIsValid()
        {
            //Arrange
            var input = new IJustEatRequest<JustEatRestaurantsBasicRequest> { TbodyRequired = false, RequestBody = null };
            _baseRestApiServiceMock
                .Mock_SendApiRequestAsync_With_Custom_Response<RestaurantsGeneralData, object>(new RestaurantsGeneralData { Restaurants = new List<RestaurantMainItem>()}, null, true);

            //Act
            var result = await _justEatApiIntegrationService.GetRestaurantListForUserByDeliveryPostcodeAsync(input);

            //Arrange
            Assert.NotNull(result);
            Assert.NotNull(result.Restaurants);
            Assert.Null(result.Errors);
            Assert.True(result.Success);
        }
    }
}
