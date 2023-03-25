using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.DataLayer.Entities
{
    public class ClubCard
    {
        [ForeignKey("Client")]
        [Key]
        public int Id { get; set; }
        public string ClubCardNumber { get; set; }
        //public int ClientId { get; set; }
        //[ForeignKey("ClientId")]
        public virtual Client Client {get;set;}
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public ClubCard()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}
