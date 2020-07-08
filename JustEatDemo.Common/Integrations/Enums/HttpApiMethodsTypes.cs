using System.ComponentModel;

namespace JustEatDemo.Common.Integrations
{
    public enum HttpApiMethodsType
    {
        GET,
        POST,
        PUT,
        DELETE,
        PATCH
    }

    public static class HttpApiMethodsTypeConverter
    {
        public static string EnumToString(HttpApiMethodsType methodType)
        {
            switch (methodType)
            {
                case HttpApiMethodsType.GET:
                    return "GET";
                case HttpApiMethodsType.POST:
                    return "POST";
                case HttpApiMethodsType.PUT:
                    return "PUT";
                case HttpApiMethodsType.DELETE:
                    return "DELETE";
                case HttpApiMethodsType.PATCH:
                    return "PATCH";
                default:
                    throw new InvalidEnumArgumentException($"Wrong method type: {methodType}");
            }
        }

        public static HttpApiMethodsType StringToEnum(string methodType)
        {
            if (string.IsNullOrWhiteSpace(methodType))
            {
                throw new InvalidEnumArgumentException($"Wrong method type for enum conversion: {methodType}");
            }

            switch (methodType.ToUpper())
            {
                case "GET":
                    return HttpApiMethodsType.GET;
                case "POST":
                    return HttpApiMethodsType.POST;
                case "PUT":
                    return HttpApiMethodsType.PUT;
                case "DELETE":
                    return HttpApiMethodsType.DELETE;
                case "PATCH":
                    return HttpApiMethodsType.PATCH;
                default:
                    throw new InvalidEnumArgumentException($"Wrong method type for enum conversion: {methodType}");
            }
        }
    }
}
