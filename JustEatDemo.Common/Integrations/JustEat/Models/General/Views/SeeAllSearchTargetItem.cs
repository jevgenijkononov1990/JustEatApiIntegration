using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General.Views
{
    public class SeeAllSearchTargetItem
    {
        public List<object> CuisineFilters { get; set; } 
        public string SortOrder { get; set; }
        public List<object> Refinements { get; set; }
        //No idea what kind of object will be used so just in case for now object
    }
}
