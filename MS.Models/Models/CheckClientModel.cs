using System.ComponentModel.DataAnnotations;
using MS.Localization;

namespace MS.Models.Models
{
    public class CheckClientModel
    {
        [Required, Display(Name = "Email", ResourceType = typeof(Strings)), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,Display(Name = "ClubCardNumber", ResourceType = typeof(Strings))]
        public string ClubCard { get; set; }
        [Display(Name = "VerificationСode", ResourceType = typeof(Strings))]
        public int? VerivicationCode { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Password", ResourceType = typeof(Strings))]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Strings))]
        [Compare("Password", ErrorMessageResourceName = "PasswordMustMatch", ErrorMessageResourceType = typeof(Strings))]
        public string ConfirmPassword { get; set; }
    }
}