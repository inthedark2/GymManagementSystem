using MS.DataLayer.Entities;
using System.Linq;


namespace MS.DataLayer.Abstract
{
    public interface ICardRepository : IRepository
    {
        ClubCard GetByNumber(string number);
        IQueryable<ClubCard> ClubCards();
        ClubCard AddNewCard(string cardNumber, Client client);
    }
    
}
