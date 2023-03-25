using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MS.Api.Models
{
    public class RecordForTrainingModel
    {
        public int Id { get; set; }
       // public virtual UserModel Trainer { get; set; }
        public virtual ClientModel Client { get; set; }
        public DateTime TrainingDate { get; set; }
        public int StatusId { get; set; }
    }
}