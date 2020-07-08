using JustEatDemo.Domain.Restaurant;
using JustEatDemo.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using JustEatDemo.Domain.Restaurant.Models;
using System.Threading.Tasks;
using JustEatDemo.Domain.CommonLogic;
using JustEatDemo.Common.Integrations.JustEat;
using System.ComponentModel;
using JustEatDemo.Common.Integrations.JustEat.Models.Responses;
using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations.JustEat.Models.General;

namespace JustEatDemo.Tests.ServiceTests
{
    public class RestaurantServiceTests
    {
        private JustEatApiIntegrationServiceMock _justEatIntegrationServiceMock;
        private JustEatRepositoryMock _justEatRepositoryMock;

        private RestaurantService _restaurantService;

        public RestaurantServiceTests()
        {
            _justEatRepositoryMock = new JustEatRepositoryMock();
            _justEatIntegrationServiceMock = new JustEatApiIntegrationServiceMock();

            _restaurantService = new RestaurantService(_justEatRepositoryMock.Object, _justEatIntegrationServiceMock.Object);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void Test_RestaurantService_Constructor_ForDefense_When_OneOfTheDependn_Null_Result_ThrowException(int initScenario)
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            { 
                if(initScenario == 0)
                {
                    new RestaurantService(null, null);
                }
                if(initScenario == 1)
                {
                    new RestaurantService(_justEatRepositoryMock.Object, null);
                }
                if (initScenario == 2)
                {
                    new RestaurantService(null, _justEatIntegrationServiceMock.Object);
                }
            });

            //Arrange
            Assert.Throws<ArgumentNullException>(arrange);
        }


        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_WhenInput_Null_Result_ArgumentNullException()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = null;

            //Act
            Func<Task> act = async () => await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);

            //Arrange
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_When_JustEatRepository_ThrowsException_ResultException()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = new DeliveryDetailsInfoRequest {  UserData = new UserCredentials()};
            _justEatRepositoryMock.Mock_GetSettings_ThrowException();
            
            //Act
            Func<Task> act = async () => await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);
            
            //Arrange
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_When_JustEatRepository_Null_ArgumentNullException()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = new DeliveryDetailsInfoRequest { UserData = new UserCredentials() };
            _justEatRepositoryMock.Mock_GetSettings_CustomReturn(null);

            //Act
            Func<Task> act = async () => await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);

            //Arrange
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_When_ApiMethodConverterError_Result_InvalidEnumArgumentException()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = new DeliveryDetailsInfoRequest { UserData = new UserCredentials() };
            _justEatRepositoryMock.Mock_GetSettings_CustomReturn(new JustEatIntegrationSettings
            {
                CoreUrl = "abc",
                RestaurantsRequestType = "abcasda"
            });

            //Act
            Func<Task> act = async () => await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);

            //Arrange
            await Assert.ThrowsAsync<InvalidEnumArgumentException>(act);
        }

        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_When_justEatIntegrationService_ResultNull_ArgumentNullException()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = new DeliveryDetailsInfoRequest { UserData = new UserCredentials() };
            _justEatRepositoryMock.Mock_GetSettings_CustomReturn(new JustEatIntegrationSettings
            {
                RestaurantsRequestType = "POST"
            });

            //Act
            Func<Task> act = async () => await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);

            //Arrange
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_When_justEatIntegrationServiceThrowsEx_ResultEx()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = new DeliveryDetailsInfoRequest { UserData = new UserCredentials() };
            _justEatRepositoryMock.Mock_GetSettings_CustomReturn(new JustEatIntegrationSettings
            {
                RestaurantsRequestType = "POST"
            });
            _justEatIntegrationServiceMock.Mock_GetRestaurantDetailsByUserAndPostcode_ThrowsException();

            //Act
            Func<Task> act = async () => await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);

            //Arrange
            await Assert.ThrowsAsync<Exception>(act);
        }

        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_When_justEatIntegrationService_FinishCorrect_ButRestaurantsObjectIsNull_Result_NoError()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = new DeliveryDetailsInfoRequest { UserData = new UserCredentials() };
            _justEatRepositoryMock.Mock_GetSettings_CustomReturn(new JustEatIntegrationSettings
            {
                RestaurantsRequestType = "POST"
            });

            _justEatIntegrationServiceMock.Mock_GetRestaurantDetailsByUserAndPostcode_CustomReturn(new JustEatRestarauntResponseData
            {
                Restaurants = null,
                Errors = new List<ErrorData>
                {
                    new ErrorData
                    {
                        Message  = "aa", Token = "abcas"
                    }
                }
            });

            //Act
            DeliveryDetailsInfoResponse result = await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);

            //Arrange
            Assert.NotNull(result);
            Assert.NotNull(result.Error);
            Assert.Null(result.Restaurants);
            Assert.IsType<DeliveryDetailsInfoResponse>(result);
        }

        [Fact]
        public async Task Test_GetRestaurantsListByUserAndPostCodeAsync_When_justEatIntegrationService_FinishCorrect_RestarauntDataIsNotNull_Result_NoError()
        {
            //Arrange
            DeliveryDetailsInfoRequest input = new DeliveryDetailsInfoRequest { UserData = new UserCredentials() };
            _justEatRepositoryMock.Mock_GetSettings_CustomReturn(new JustEatIntegrationSettings
            {
                RestaurantsRequestType = "POST"
            });

            _justEatIntegrationServiceMock.Mock_GetRestaurantDetailsByUserAndPostcode_CustomReturn(new JustEatRestarauntResponseData
            {
                Restaurants = new List<RestaurantShortDetails>(),
                Errors = null
            });

            //Act
            DeliveryDetailsInfoResponse result = await _restaurantService.GetRestaurantsListByUserAndPostCodeAsync(input);

            //Arrange
            Assert.NotNull(result);
            Assert.Null(result.Error);
            Assert.NotNull(result.Restaurants);
            Assert.IsType<DeliveryDetailsInfoResponse>(result);
        }
    }
}
