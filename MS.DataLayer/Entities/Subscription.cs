using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.DataLayer.Entities
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //public int ClubCardId { get; set; }
        //[ForeignKey("ClubCardId")]
        public virtual ClubCard ClubCard { get; set; }
        //public int SubscriptionTypeId { get; set; }
        //[ForeignKey("SubscriptionTypeId")]
        public virtual SubscriptionType SubscriptionType { get; set; }
        public int StatusId { get; set; }
        public DateTime? FrozenFrom { get; set; }
        public DateTime? FrozenTo { get; set; }
        public int LeftFrozenDays { get; set; }
        public bool IsVisible { get; set; }
    }
}
