using System.Linq;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.IServices
{
    public interface ICardService
    {
        ClubCard GetByNumber(string number);
        IQueryable<ClubCard> ClubCards();
        ClubCard AddNewCard(string cardNumber, Client client);
    }
}
