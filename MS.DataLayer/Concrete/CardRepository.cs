using System;
using System.Linq;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.DataLayer.Concrete
{
    public class CardRepository : BaseRepository, ICardRepository
    {
        private readonly ManagmentSystemContext _context;

        public CardRepository(ManagmentSystemContext context)
        {
            this._context = context;
        }
        public ClubCard AddNewCard(string cardNumber, Client client)
        {
            if (GetByNumber(cardNumber) == null)
            {
                ClubCard card = new ClubCard { ClubCardNumber = cardNumber ,Client = client};
                _context.ClubCards.Add(card);
                _context.SaveChanges();
                return card;
            }
            return null;
        }

        public IQueryable<ClubCard> ClubCards()
        {
            return from data in _context.ClubCards select data;
        }

        public ClubCard GetByNumber(string number)
        {
            return _context.ClubCards.SingleOrDefault(c => c.ClubCardNumber == number);
        }
    }
}
