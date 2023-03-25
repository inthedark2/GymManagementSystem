using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.DataLayer.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; } 
        public virtual ClubCard ClubCardId { get; set; }
        public virtual Gender GenderId { get; set; }
        public int ValidationCode { get; set; }
        public string PhotoPath { get; set; }
    }
}
