using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General.CuisineSets
{
    public class CuisineSetItem : ISet
    {
        public string Id { get; set; }
        public string Name { get; set ; }
        public string Type { get; set; }
        public List<CuisineItem> Cuisines { get; set; }
    }
}
