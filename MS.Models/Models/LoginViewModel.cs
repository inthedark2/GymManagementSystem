using System.ComponentModel.DataAnnotations;
using MS.Localization;

namespace MS.Models.Models
{
    public class LoginViewModel
    {
        [Required,DataType(DataType.EmailAddress), Display(Name = "Email", ResourceType = typeof(Strings))]
        public string Email { get; set; }
        [Required,DataType(DataType.Password), Display(Name = "Password", ResourceType = typeof(Strings))]
        public string Password { get; set; }
        [Required, Display(Name = "ChooseWhoAreYou", ResourceType = typeof(Strings))]
        public bool IsClient { get; set; }
    }
}