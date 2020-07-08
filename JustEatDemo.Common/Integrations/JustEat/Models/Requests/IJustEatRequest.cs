using JustEatDemo.Common.Integrations.Enums;
using JustEatDemo.Common.Integrations.Models;
using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models
{
    public class IJustEatRequest<TBody> where TBody : class
    {
        public TBody RequestBody { get; set; }
        public bool TbodyRequired { get; set; }
        public string CoreUrl { get; set; }
        public string Endpoint { get; set; }
        public string Query { get; set; }
        public List<CustomHeader> Header { get; set; }
        public HttpApiMethodsType ApiMethod { get; set;}
        public HttpAuthType AuthType { get; set; }
    }
}
