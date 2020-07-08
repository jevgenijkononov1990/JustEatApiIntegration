
using System.ComponentModel;

namespace JustEatDemo.Common.Integrations.Enums
{
    public enum HttpAuthType
    {
        [Description("No Auth")]
        NoAuth,
        [Description("API KEY")]
        ApiKey,
        [Description("Bearer Token")]
        BearenToken,
        [Description("Basic Auth")]
        BasicAuth,
        [Description("Digest Auth")]
        DigestAuth,
        [Description("AWS Signature")]
        AWSSignature,
        [Description("OAuth 1.0")]
        OAuth_v1,
        [Description("OAuth 2.0")]
        OAuth_v2,
    }
}
