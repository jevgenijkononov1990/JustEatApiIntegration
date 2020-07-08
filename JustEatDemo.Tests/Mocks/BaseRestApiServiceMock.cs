using JustEatDemo.Common.Integrations;
using JustEatDemo.Common.Integrations.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace JustEatDemo.Tests.Mocks
{
    public class BaseRestApiServiceMock : Mock<IBaseRestApiService>
    {

        private (TResponse responseModel, HttpResponseMessage requestResponse, bool deserializationSuccess) ProcessResponse<TResponse>(TResponse responseModelCustom, HttpResponseMessage requestResponseCustom, bool deserializationSuccessCustom)
        where TResponse : class
        {
            return (responseModelCustom, requestResponseCustom, deserializationSuccessCustom);
        }

        public BaseRestApiServiceMock Mock_SendApiRequestAsync_With_Custom_Response<TResponse,TRequest >
        (
            TResponse responseModel,
            HttpResponseMessage requestResponse,
            bool deserializationSuccess
        )
        where TRequest : class
        where TResponse : class
        {
            Setup(x => x.SendApiRequestAsync<TResponse, TRequest>
            (
                It.IsAny<string>(),
                It.IsAny<TRequest>(),
                It.IsAny<List<CustomHeader>>(),
                It.IsAny<HttpApiMethodsType>(),
                It.IsAny<bool>() ,
                It.IsAny<HttpAuth>()
            )).ReturnsAsync(ProcessResponse(responseModel, requestResponse, deserializationSuccess));
            return this;
        }

        public BaseRestApiServiceMock Mock_SendApiRequestAsyncThrowsException<TResponse,TRequest>
        (
            TResponse responseModel,
            HttpResponseMessage requestResponse,
            bool deserializationSuccess
        )
        where TRequest : class
        where TResponse : class
        {
            Setup(x => x.SendApiRequestAsync<TResponse, TRequest>
            (
                It.IsAny<string>(),
                It.IsAny<TRequest>(),
                It.IsAny<List<CustomHeader>>(),
                It.IsAny<HttpApiMethodsType>(),
                It.IsAny<bool>(),
                It.IsAny<HttpAuth>()

            )).ThrowsAsync(new Exception());
            return this;
        }

        public BaseRestApiServiceMock Mock_SendApiRequestAsync_SkipTClassInit_ThrowsException()
        {
            Setup(x => x.SendApiRequestAsync<object, object>
            (
                It.IsAny<string>(),
                It.IsAny<object>(),
                It.IsAny<List<CustomHeader>>(),
                It.IsAny<HttpApiMethodsType>(),
                It.IsAny<bool>(),
                It.IsAny<HttpAuth>()

            )).ThrowsAsync(new Exception());
            return this;
        }
    }
}
