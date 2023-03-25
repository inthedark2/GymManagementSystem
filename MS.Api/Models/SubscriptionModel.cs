using System;

namespace MS.Api.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ClubCardModel ClubCardId { get; set; }
        public virtual SubscriptionTypeModel SubscriptionTypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime? FrozenFrom { get; set; }
        public DateTime? FrozenTo { get; set; }
        public int LeftFrozenDays { get; set; }
    }
}