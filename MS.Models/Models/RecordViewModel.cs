using System;
using System.ComponentModel.DataAnnotations;
using MS.Localization;

namespace MS.Models.Models
{
    public class RecordViewModel
    {
        public int RecordId { get; set; }
        [Required, Display(Name = "Name", ResourceType = typeof(Strings))]
        public string Name { get; set; }
        [Required, Display(Name = "Surname", ResourceType = typeof(Strings))]
        public string Surname { get; set; }
        [Required, Display(Name = "Email", ResourceType = typeof(Strings))]
        public string Email { get; set; }
        [Required, Display(Name = "PhoneNumber", ResourceType = typeof(Strings))]
        public string PhoneNumber { get; set; }
        [Required, Display(Name = "TrainingDate", ResourceType = typeof(Strings))]
        public string Time { get; set; }
        [Required, Display(Name = "StatusID")]
        public int StatusId { get; set; }
    }
}