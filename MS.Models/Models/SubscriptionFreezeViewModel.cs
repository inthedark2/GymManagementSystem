using System;
using System.ComponentModel.DataAnnotations;

namespace MS.Models.Models
{
    public class SubscriptionFreezeViewModel
    {
        [Required]
        public int SubscriptionId { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        public int LeftFrozenDays { get; set; }
    }
}
