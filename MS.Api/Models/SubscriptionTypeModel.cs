using System.Collections.Generic;

namespace MS.Api.Models
{
    public sealed class SubscriptionTypeModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<SubscriptionModel> Subscriptions { get; set; }
        public int MaxFrozenDays { get; set; }

        public SubscriptionTypeModel()
        {
            Subscriptions = new List<SubscriptionModel>();
        }
    }
}