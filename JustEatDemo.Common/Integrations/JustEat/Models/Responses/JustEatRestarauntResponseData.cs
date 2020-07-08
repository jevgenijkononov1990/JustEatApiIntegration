using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations.JustEat.Models.General;
using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.Responses
{
    public class JustEatRestarauntResponseData 
    {
        public List<RestaurantShortDetails> Restaurants { get; set; }
        public bool Success { get; set; } = false;
        public List<ErrorData> Errors { get; set; } 
    }

}
