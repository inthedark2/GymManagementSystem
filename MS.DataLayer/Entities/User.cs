using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.DataLayer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        //public int RoleId { get; set; }
        //[ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public string PhotoPath{get;set;}
    }
}
