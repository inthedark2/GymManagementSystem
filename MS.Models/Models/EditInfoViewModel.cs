using MS.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MS.Models.Models
{
    public class EditInfoViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Strings))]
        public string Name { get; set; }
        [Display(Name = "Surname", ResourceType = typeof(Strings))]
        public string Surname { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Strings))]
        public string Email { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(Strings))]
        public string PhoneNumber { get; set; }
        public string PhotoPath { get; set; }
        public DateTime DateOfBirth { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public int UserTypeId { get; set; }
    }
}
