using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General.Views
{
    public class ViewDataItem
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public SeeAllSearchTargetItem SeeAllSearchTarget { get; set; }
        public List<string> FocusedProperties { get; set; }
    }
}
