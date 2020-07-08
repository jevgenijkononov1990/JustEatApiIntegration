using JustEatDemo.Common.Integrations;
using JustEatDemo.Domain.Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JustEatDemo.Tests.IntegrationTests.RestaurantController
{
    public class GetRestaurantsDeliveryDataByPostCodeTests
    {
        private readonly HttpClient _client;
        private readonly RestaurantServiceMock _restarauntServiceMock;
        private readonly HttpRequestMessageWrapper _controllerRequester;
        private const string _apiPath = "api/restaurant/";
        private const HttpApiMethodsType _apiMethodType = HttpApiMethodsType.GET;

        public GetRestaurantsDeliveryDataByPostCodeTests()
        {
            #region UnitTestPreparation

            _controllerRequester = new HttpRequestMessageWrapper();
            _restarauntServiceMock = new RestaurantServiceMock();
            _client = new IntegrationTestServer(_restarauntServiceMock.Object).CreateClient();

            #endregion
        }

        [Theory]
        [InlineData(null,null,null)]
        [InlineData("", "", "")]
        [InlineData("aaa", "aaa", null)]
        [InlineData("aaa", null, "aaa")]
        [InlineData(null, "aaa", "  ")]
        public async Task GetRestaurantsDeliveryDataByPostCode_Test_When_RequestBodyNull_Result_BadRequest(string userName, string userPassword, string requestPostCode)
        {
            //Arrange
            var requestUrl = _apiPath + $"?username={userName}&password={userPassword}&postcode={requestPostCode}";
            
            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(_apiMethodType, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("aaa", "aaa", "aaa")]
        public async Task GetRestaurantsDeliveryDataByPostCode_Test_When_MainServiceThrowsException_Result_InternalServerError(string userName, string userPassword, string requestPostCode)
        {
            //Arrange
            var requestUrl = _apiPath + $"?username={userName}&password={userPassword}&postcode={requestPostCode}";
            _restarauntServiceMock.Mock_GetRestaurantsListByUserAndPostCodeAsyn_ThrowsException();

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(_apiMethodType, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Theory]
        [InlineData("aaa", "aaa", "aaa")]
        public async Task GetRestaurantsDeliveryDataByPostCode_Test_When_MainServiceReturnNull_Result_BadRequest(string userName, string userPassword, string requestPostCode)
        {
            //Arrange
            var requestUrl = _apiPath + $"?username={userName}&password={userPassword}&postcode={requestPostCode}";
            _restarauntServiceMock.Mock_GetRestaurantsListByUserAndPostCodeAsyns_CustomReturn((DeliveryDetailsInfoResponse)null);

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(_apiMethodType, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("aaa", "aaa", "aaa")]
        public async Task GetRestaurantsDeliveryDataByPostCode_Test_When_MainServiceReturnProperObject_Result_Success(string userName, string userPassword, string requestPostCode)
        {
            //Arrange
            var requestUrl = _apiPath + $"?username={userName}&password={userPassword}&postcode={requestPostCode}";
            _restarauntServiceMock.Mock_GetRestaurantsListByUserAndPostCodeAsyns_CustomReturn(new DeliveryDetailsInfoResponse());

            //Act
            HttpRequestMessage request = _controllerRequester.CreateHttpRequestMessage(_apiMethodType, requestUrl, (object)null);
            HttpResponseMessage response = await _client.SendAsync(request);

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
