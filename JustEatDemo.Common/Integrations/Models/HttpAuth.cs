using JustEatDemo.Common.Integrations.Enums;

namespace JustEatDemo.Common.Integrations.Models
{
    public class HttpAuth
    {
        public HttpAuthType AuthType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
    }
}
