using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MS.DataLayer.Entities
{
    public class SubscriptionType
    {
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public int MaxFrozenDays { get; set; }
        public int MonthCount { get; set; }
        public SubscriptionType()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}
