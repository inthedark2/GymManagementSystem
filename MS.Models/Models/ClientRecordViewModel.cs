using System;
using System.ComponentModel.DataAnnotations;
using MS.Localization;

namespace MS.Models.Models
{
    public class ClientRecordViewModel
    {
        public int RecordId { get; set; }
        [Display(Name = "TrainingDate", ResourceType = typeof(Strings))]
        public string TrainingDate { get; set; }
        [Display(Name = "Trainer", ResourceType = typeof(Strings))]
        public string TrainerFullName { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Strings))]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
