using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.DataLayer.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public virtual SubscriptionType SubscriptionType { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual Client Client { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PaymentStatusId { get; set; }
    }
}
