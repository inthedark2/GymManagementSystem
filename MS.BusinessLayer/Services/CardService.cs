using System.Linq;
using MS.BusinessLayer.IServices;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public ClubCard GetByNumber(string number)
        {
            return _cardRepository.GetByNumber(number);
        }

        public IQueryable<ClubCard> ClubCards()
        {
            return _cardRepository.ClubCards();
        }

        public ClubCard AddNewCard(string cardNumber, Client client)
        {
            return _cardRepository.AddNewCard(cardNumber, client);
        }
    }
}
