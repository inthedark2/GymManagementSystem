using System;

namespace MS.Models.Models
{
    public class ClientViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public string SubscriptionStatus { get; set; }
        public string SubscriptionType { get; set; }
        public int SubscriptionFrozenDayLeft { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ClubCardNumber { get; set; }
        public int LeftFreezeDays { get; set; }
        public string PhotoPath { get; set; }
    }
}