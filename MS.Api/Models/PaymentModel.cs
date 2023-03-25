using System;

namespace MS.Api.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public virtual SubscriptionTypeModel SubscriptionType { get; set; }
        public virtual SubscriptionModel Subscription { get; set; }
        public virtual ClientModel Client { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}