using JustEatDemo.Common.Integrations.JustEat.Models.General.CuisineSets;
using JustEatDemo.Common.Integrations.JustEat.Models.General.MetaData;
using JustEatDemo.Common.Integrations.JustEat.Models.General.Restaurant;
using JustEatDemo.Common.Integrations.JustEat.Models.General.Sets;
using JustEatDemo.Common.Integrations.JustEat.Models.General.Views;
using System;
using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General
{
    public class RestaurantsGeneralData
    {
        public List<RestaurantSetItem> RestaurantSets { get; set; }
        public List<CuisineSetItem> CuisineSets { get; set; }
        public List<ViewItem> Views { get; set; }
        public List<RestaurantMainItem> Restaurants { get; set; }
        public string ShortResultText { get; set; }
        public string Area { get; set; }
        public List<Object> Errors { get; set; }
        public bool HasErrors { get; set; }
        public MetaDataItem MetaData { get; set; }
    }
}
