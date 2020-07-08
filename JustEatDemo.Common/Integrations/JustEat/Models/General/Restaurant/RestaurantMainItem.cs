using System.Collections.Generic;

namespace JustEatDemo.Common.Integrations.JustEat.Models.General.Restaurant
{
    public class RestaurantMainItem
    {
        public List<object> Badges { get; set; } //public List<BadgeItem> Badges { get; set; }
        public double Score { get; set; }
        public double? DriveDistance { get; set; }
        public bool DriveInfoCalculated { get; set; }
        public string NewnessDate { get; set; }
        public long? DeliveryMenuId { get; set; }
        public string DeliveryOpeningTime { get; set; }
        public double? DeliveryCost { get; set; }
        public float? MinimumDeliveryValue { get; set; }
        public int? DeliveryTimeMinutes { get; set; }
        public int? DeliveryWorkingTimeMinutes { get; set; }
        public string OpeningTime { get; set; }
        public string OpeningTimeIso { get; set; }
        public bool SendsOnItsWayNotifications { get; set; }
        public double RatingAverage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<object> Tags { get; set; }         //public List<TagItem> Tags { get; set; }
        public List<ScoreMetadataItem> ScoreMetadata { get; set; }
        public DeliveryEtaMinutesItem DeliveryEtaMinutes { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public List<CuisineTypeItem> CuisineTypes { get; set; }
        public string Url { get; set; }
        public bool IsOpenNow { get; set; }
        public bool IsPremier { get; set; }
        public bool IsSponsored { get; set; }
        public bool IsTemporaryBoost { get; set; }
        public int? SponsoredPosition { get; set; }
        public bool IsNew { get; set; }
        public bool IsTemporarilyOffline { get; set; }
        public string ReasonWhyTemporarilyOffline { get; set; }
        public string UniqueName { get; set; }
        public bool IsCloseBy { get; set; }
        public bool IsHalal { get; set; }
        public bool IsTestRestaurant { get; set; }
        public long DefaultDisplayRank { get; set; }
        public bool IsOpenNowForDelivery { get; set; }
        public bool IsOpenNowForCollection { get; set; }
        public double RatingStars { get; set; }
        public List<LogoItem> Logo { get; set; }
        public List<DealItem> Deals{ get; set; }
        public int NumberOfRatings{ get; set; }
        public bool ShowSmiley{ get; set; }
        public int? SmileyResult{ get; set; }
        public string SmileyUrl{ get; set; }
        public bool SmileyElite{ get; set; }
    }
}
