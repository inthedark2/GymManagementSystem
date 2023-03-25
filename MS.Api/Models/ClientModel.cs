namespace MS.Api.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public ClubCardModel ClubCardId { get; set; }
        public GenderModel GenderId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoPath { get; set; }
    }
}