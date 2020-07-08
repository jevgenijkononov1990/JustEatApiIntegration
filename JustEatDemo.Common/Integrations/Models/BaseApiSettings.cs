using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.Models
{
    public class BaseApiSettings<TRequest> where TRequest : class
    {
        public readonly string Url;
        public readonly TRequest Request;
        public readonly List<CustomHeader> Header;
        public readonly HttpApiMethodsType ApiMethodName;
        public readonly bool DoNotDeserializeOutput;
        public readonly HttpAuth AuthRequest;

        BaseApiSettings(string url, TRequest request, List<CustomHeader> header = null, HttpApiMethodsType apiMethodName = HttpApiMethodsType.POST, bool doNotDeserializeOutput = true, HttpAuth authorizationType = null)
        {
            Url = url;
            Request = request;
            Header = header;
            ApiMethodName = apiMethodName;
            DoNotDeserializeOutput = doNotDeserializeOutput;
            AuthRequest = authorizationType;
        }
    }
}
