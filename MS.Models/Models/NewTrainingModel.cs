using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MS.Models.Models
{
    public class NewTrainingModel
    {
        public List<TrainerModelForNewTraining> Trainers { get; set; }
        [Required]
        public int TrainerId { get; set; }
        [Required]
        public string TrainingDate { get; set; }
        [Required]
        public TimeSpan TrainingTime { get; set; }
    }
}
