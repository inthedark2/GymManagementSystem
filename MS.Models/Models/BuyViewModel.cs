using System.ComponentModel.DataAnnotations;
using MS.Localization;

namespace MS.Models.Models
{
    public class BuyViewModel
    {
        [Required(ErrorMessageResourceName = "Error_SelectSubscriptionType", ErrorMessageResourceType = typeof(Strings))]
        public int SubscriptionTypeId { get; set; }
    }
}
