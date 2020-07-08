using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General.MetaData
{
    public class MetaDataItem
    {
        public SearchedTermsItem SearchedTerms { get; set; }
        public List<TagDetailsItem> TagDetails { get; set; }
    }
}
