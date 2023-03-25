using System.Linq;
using MS.BusinessLayer.IServices;
using MS.Common.Enums;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client AddClient(string name, string surname, string secondname, string phone, string email, Gender gender)
        {
            return _clientRepository.AddClient(name, surname, secondname, phone, email, gender);
        }

        public Client AddClient(string name, string surname, string secondname, string phone, string email, EGenderType genderType)
        {
            return _clientRepository.AddClient(name, surname, secondname, phone, email, genderType);
        }

        //public Client EditClient(string name, string surname, string phone, string email, Gender gender, ClubCard card)
        //{
        //    return _clientService.EditClient(name, surname, phone, email, gender, card);
        //}

        public Client GetClientByEmail(string email)
        {
            return _clientRepository.GetClientByEmail(email);
        }

        public Client GetClientByEmailAndCard(string email, string card)
        {
            return _clientRepository.GetClientByEmailAndCard(email, card);
        }

        public Client GetClientByClubCard(ClubCard card)
        {
            return _clientRepository.GetClientByClubCard(card);
        }

        public bool RemoveClientByClubCard(ClubCard card)
        {
            return _clientRepository.RemoveClientByClubCard(card);
        }

        public IQueryable<Client> Clients()
        {
            return _clientRepository.Clients();
        }

        public bool AddCardToClient(ClubCard card)
        {
            return _clientRepository.AddCardToClient(card);
        }

        public Client RegisterClient(string email, string password)
        {
            return _clientRepository.RegisterClient(email, password);
        }

        public bool CheckClientPassword(string email, string password)
        {
            return _clientRepository.CheckClientPassword(email, password);
        }

        public bool SetVerificationCode(string email, int code)
        {
            return _clientRepository.SetVerificationCode(email, code);
        }

        public int GetUserType(string email)
        {
            return _clientRepository.GetUserType(email);
        }
        public bool UpdateClient(string name, string surname, string secondname, string phonenumber, string email, string clubcardnumber)
        {
            return _clientRepository.UpdateClient(name, surname, secondname, phonenumber, email, clubcardnumber);
        }
    }
}
