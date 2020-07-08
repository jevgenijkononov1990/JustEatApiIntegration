using JustEatDemo.Common.Integrations.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JustEatDemo.Common.Integrations
{
    public class IntegrationApiClient : IBaseRestApiService
    {
        public IntegrationApiClient()
        {

        }

        public async Task<(TResponse responseModel, HttpResponseMessage requestResponse, bool deserializationSuccess)> SendApiRequestAsync<TResponse,TRequest>
            (string url, TRequest requestData, List<CustomHeader> requestHeaders = null, HttpApiMethodsType apiMethodName = HttpApiMethodsType.POST, bool doNotDeserializeOutput = true, HttpAuth authRequest = null) 
            where TRequest : class where TResponse : class
        {
            HttpRequestMessageWrapper httpRequestMessageWrapper = new HttpRequestMessageWrapper();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;

            try
            {
                if (authRequest != null)
                {
                    httpRequestMessageWrapper.HandleHttpClientAuth(client, authRequest.AuthType, authRequest.Username, authRequest.Password, authRequest.ApiKey);
                }

                HttpRequestMessage request = httpRequestMessageWrapper.CreateHttpRequestMessage<TRequest>(apiMethodName, url, requestData, requestHeaders);

                response = await client.SendAsync(request);
               
                if(response != null && response.IsSuccessStatusCode)
                {
                    if (doNotDeserializeOutput)
                    {
                        return (null, response, false);
                    }
                    else
                    {
                        return (await httpRequestMessageWrapper.DeserializeHttpRequestMessageAsync<TResponse>(response), response, true);
                    }
                }
                else
                {
                    //log
                    return (null, null, false);
                }
            }
            catch (Exception ex)
            {
                //log
                Console.WriteLine(ex.Message);
                return (null, response, false);
            }
        }


        public async Task<(TResponse responseModel, HttpResponseMessage requestResponse, bool deserializationSuccess)> SendApiRequestAsync<TResponse, TRequest>(BaseApiSettings<TRequest> requestSettings)
            where TResponse : class
            where TRequest : class
        {
            HttpRequestMessageWrapper httpRequestMessageWrapper = new HttpRequestMessageWrapper();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;

            try
            {
                if(requestSettings == null)
                {
                    //log
                    return (null, response, false);
                }

                if (requestSettings.AuthRequest != null)
                {
                    httpRequestMessageWrapper.HandleHttpClientAuth(client, requestSettings.AuthRequest.AuthType, requestSettings.AuthRequest.Username, requestSettings.AuthRequest.Password, requestSettings.AuthRequest.ApiKey);
                }

                HttpRequestMessage request = httpRequestMessageWrapper.CreateHttpRequestMessage<TRequest>(requestSettings.ApiMethodName, requestSettings.Url, requestSettings.Request, requestSettings.Header);

                response = await client.SendAsync(request);

                if (response != null && response.IsSuccessStatusCode)
                {
                    if (requestSettings.DoNotDeserializeOutput)
                    {
                        return (null, response, false);
                    }
                    else
                    {
                        return (await httpRequestMessageWrapper.DeserializeHttpRequestMessageAsync<TResponse>(response), response, true);
                    }
                }
                else 
                {
                    //log
                    return (null, null, false);
                }
            }
            catch (Exception ex)
            {
                //log
                Console.WriteLine(ex.Message);
                return (null, response, false);
            }
        }
    }
}
