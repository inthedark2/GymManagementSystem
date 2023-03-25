using System;
using System.ComponentModel.DataAnnotations;
using MS.Localization;

namespace MS.Models.Models
{
    public class SubscriptionsViewModel
    {
        [Required, Display(Name = "SubscriptionId")]
        public int SubscriptionId { get; set; }
        [Required, Display(Name = "SubscroptionType",ResourceType =typeof(Strings))]
        public string SubscroptionType { get; set; }
        [Required, Display(Name = "StartDate", ResourceType = typeof(Strings))]
        public string StartDate { get; set; }
        [Required, Display(Name = "EndDate", ResourceType = typeof(Strings))]
        public string EndDate { get; set; }
        [Required, Display(Name = "SubscriptionStatus", ResourceType = typeof(Strings))]
        public string SubscriptionStatus { get; set; }
        public int SubscriptionStatusId { get; set; }
    }
}
