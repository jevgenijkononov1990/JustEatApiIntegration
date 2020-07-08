using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat
{
    public class JustEatIntegrationSettings
    {
        public string CoreUrl { get; set; } = null;
        public string RestaurantsEndpoint { get; set; } = null;
        public string RestaurantsRequestType { get; set; } = null;
        public List<RequestHeader> RequestHeader { get; set; }
        public List<QueryParameter> QueryParameters { get; set; }
    }

    public class RequestHeader
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class QueryParameter
    {
        public string Key { get; set; }
        public string Description { get; set; }
    }

}
