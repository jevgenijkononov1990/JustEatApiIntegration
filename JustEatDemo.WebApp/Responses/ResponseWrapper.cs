using JustEatDemo.Common.General;

namespace JustEatDemo.WebApp.Responses
{
    public class ResponseWrapper<TModel> where TModel : class
    {
        public TModel Result;

        public ErrorData Error;

        public ResponseWrapper()
        {
            Error = new ErrorData();
        }
    }
}
