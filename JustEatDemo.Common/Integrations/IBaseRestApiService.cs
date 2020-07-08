using JustEatDemo.Common.Integrations.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JustEatDemo.Common.Integrations
{
    public interface IBaseRestApiService
    {
        Task<(TResponse responseModel, HttpResponseMessage requestResponse, bool deserializationSuccess)> SendApiRequestAsync<TResponse, TRequest>
                   (string url, TRequest requestData, List<CustomHeader> requestHeaders = null, HttpApiMethodsType apiMethodName = HttpApiMethodsType.POST, bool doNotDeserializeOutput = true, HttpAuth authRequest = null)
                   where TRequest : class where TResponse : class;

        Task<(TResponse responseModel, HttpResponseMessage requestResponse, bool deserializationSuccess)> SendApiRequestAsync<TResponse, TRequest>(BaseApiSettings<TRequest> requestSettings)
           where TRequest : class where TResponse : class;
    }

}


