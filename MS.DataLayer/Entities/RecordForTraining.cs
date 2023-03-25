using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.DataLayer.Entities
{
    public class RecordForTraining
    {
        [Key]
        public int Id { get; set; }
        //public int TrainerId { get; set; }
        //[ForeignKey("TrainerId")]
        public virtual User Trainer { get; set; }
        //public int ClientId { get; set; }
        //[ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public DateTime TrainingDate { get; set; }
        public int StatusId { get; set; }
    }
}
