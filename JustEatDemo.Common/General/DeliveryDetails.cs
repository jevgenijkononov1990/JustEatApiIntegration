
namespace JustEatDemo.Common.General
{
    public class DeliveryDetails
    {
        public double? DriveDistance { get; set; }
        public bool DriveInfoCalculated { get; set; }
        public long? DeliveryMenuId { get; set; }
        public string DeliveryOpeningTime { get; set; }
        public double? DeliveryCost { get; set; }
        public float? MinimumDeliveryValue { get; set; }
        public int? DeliveryTimeMinutes { get; set; }
        public int? DeliveryWorkingTimeMinutes { get; set; }
        public bool IsOpenNowForDelivery { get; set; }
        public bool IsOpenNowForCollection { get; set; }
    }
}
