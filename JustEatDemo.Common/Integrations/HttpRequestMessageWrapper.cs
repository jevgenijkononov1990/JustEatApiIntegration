using JustEatDemo.Common.Integrations.Enums;
using JustEatDemo.Common.Integrations.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JustEatDemo.Common.Integrations
{
    public class HttpRequestMessageWrapper
    {
        private void ValidateInputString(string input)
        {
            if (string.IsNullOrEmpty(input)) //log
                throw new ArgumentNullException($"Validation Failure in {this.GetType().Name} due to input null");
        }

        public void HandleHttpClientAuth(HttpClient client, HttpAuthType authType, string username, string password, string apiKey)
        {
            if (client == null)
            {
                //log
                throw new ArgumentException($"Authentication process handler failure. Invalid {typeof(HttpClient)} value");
            }
            if (!System.Enum.IsDefined(typeof(HttpAuthType), authType))
            {
                //log
                throw new ArgumentException($"Invalid {typeof(HttpAuthType)} value: {authType} in Authentication process handler");
            }

            switch (authType)
            {
                case HttpAuthType.NoAuth:
                    return;
                case HttpAuthType.ApiKey:
                    throw new NotImplementedException();
                case HttpAuthType.BearenToken:
                    throw new NotImplementedException();
                case HttpAuthType.BasicAuth:
                    {
                        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                        {
                            //log
                            throw new ArgumentException($"Invalid user input values for Basic Auth process");
                        }

                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue(
                                "Basic", Convert.ToBase64String(
                                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                                       $"{username}:{password}")));
                    }
                    return;
                case HttpAuthType.DigestAuth:
                    throw new NotImplementedException();
                case HttpAuthType.AWSSignature:
                    throw new NotImplementedException();
                case HttpAuthType.OAuth_v1:
                    throw new NotImplementedException();
                case HttpAuthType.OAuth_v2:
                    throw new NotImplementedException();
            }
        }

        public HttpRequestMessage CreateHttpRequestMessage<Tbody>(HttpApiMethodsType apiMethodName, string apiUrl, Tbody requestBody, List<CustomHeader> requestHeader = null)
        {
            //defense 
            string apiMethod = HttpApiMethodsTypeConverter.EnumToString(apiMethodName);
            ValidateInputString(apiMethod);
            ValidateInputString(apiUrl);

            var request = new HttpRequestMessage(new HttpMethod(apiMethod), $"{apiUrl}");
            if (requestBody != null)
            {
                string output = JsonConvert.SerializeObject(requestBody);
                request.Content = new StringContent
                (
                    content: JsonConvert.SerializeObject(requestBody),               
                    encoding: Encoding.UTF8,
                    mediaType: "application/json"
                );
            }

            if (requestHeader != null && requestHeader.Count > 0)
            {
                foreach(var item in requestHeader)
                {
                    request.Headers.Add(item.HeaderName, item.HeaderValue);
                }
            }

            return request;
        }

        public async Task<TOutputModel> DeserializeHttpRequestMessageAsync<TOutputModel>(HttpResponseMessage httpResponseMessage) where TOutputModel : class
        {
            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TOutputModel>(jsonResponse);
            return response;
        }
    }
}
