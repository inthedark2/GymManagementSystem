using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MS.DataLayer.Entities
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; }
        public Gender()
        {
            Clients = new List<Client>();
        }
    }
}
