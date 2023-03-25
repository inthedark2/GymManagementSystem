using System.Collections.Generic;

namespace MS.Api.Models
{
    public sealed class ClubCardModel
    {
        public int Id { get; set; }
        public string ClubCardNumber { get; set; }
        public ClientModel Client { get; set; }
       // public ICollection<SubscriptionModel> Subscriptions { get; set; }

        public ClubCardModel()
        {
           // Subscriptions = new List<SubscriptionModel>();
        }
    }
}