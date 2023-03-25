using System;
using System.Linq;
using System.Web;
using MS.Common.Enums;
using MS.Common.Services;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;
using SimpleCrypto;
using System.Configuration;
using System.Web.Configuration;

namespace MS.DataLayer.Concrete
{
    public class ClientRepository : BaseRepository, IClientRepository
    {
        private readonly ManagmentSystemContext _context;

        public ClientRepository(ManagmentSystemContext context) 
        {
            _context = context;
        }

        public bool AddCardToClient(ClubCard card)
        {
            throw new NotImplementedException();
        }

        public Client AddClient(string name, string surname, string secondname, string phone, string email, EGenderType genderType)
        {
            Gender gender = _context.Genders.SingleOrDefault(g => g.Id == (int)genderType);
            var client = AddClient(name, surname, secondname, phone, email, gender);
            Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
            var cardNumber = Int64.Parse(config.AppSettings.Settings["NextClubCardNumber"].Value);

            var card = new ClubCard
            {
                Id = client.Id,
                Client = client,
                ClubCardNumber = cardNumber.ToString(),
            };

            _context.ClubCards.Add(card);
            _context.SaveChanges();

            cardNumber++;
            config.AppSettings.Settings["NextClubCardNumber"].Value = cardNumber.ToString();
            config.Save(ConfigurationSaveMode.Modified);

            return client;
        }

        public Client AddClient(string name, string surname, string secondname, string phone, string email, Gender gender)
        {
            if (GetClientByEmail(email) == null)
            {
                Client newClient = new Client { Name = name, Surname = surname, SecondName = secondname, PhoneNumber = phone, Email = email, GenderId = gender, PhotoPath = ConfigurationManager.AppSettings["DefaultImageName"] };
                _context.Clients.Add(newClient);
                _context.SaveChanges();
                return newClient;
            }
            return null;
        }

        public bool CheckClientPassword(string email, string password)
        {
            Client client = GetClientByEmail(email);
            if (client != null)
            {
                var crypto = new PBKDF2();
                if (client.Password == crypto.Compute(password, client.PasswordSalt))
                    return true;
            }
            return false;
        }

        public IQueryable<Client> Clients()
        {
            return from data in _context.Clients select data;
        }

        public Client GetClientByClubCard(ClubCard card)
        {
            return Clients().SingleOrDefault(c => c.ClubCardId == card);
        }

        public Client GetClientByEmail(string email)
        {
            return Clients().SingleOrDefault(u => u.Email == email);
        }

        public Client GetClientByEmailAndCard(string email, string card)
        {
            var client = GetClientByEmail(email);
            if (client.ClubCardId.ClubCardNumber == card)
            {
                return client;
            }
            return null;
        }

        public Client RegisterClient(string email, string password)
        {
            Client client = GetClientByEmail(email);
            if (client != null)
            {
                var crypto = new PBKDF2();
                client.Password = crypto.Compute(password);
                client.PasswordSalt = crypto.Salt;
                _context.SaveChanges();
                return client;
            }
            return null;
        }

        public bool RemoveClientByClubCard(ClubCard card)
        {
            Client client = GetClientByClubCard(card);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool SetVerificationCode(string email,int code)
        {
            Client client = GetClientByEmail(email);
            client.ValidationCode = code;
            _context.SaveChanges();
            return true;
        }

        public int GetUserType(string email)
        {
            if (GetClientByEmail(email) == null)
            {
                return (int)EUserTypes.Trainer;
            }
            return (int)EUserTypes.Client;
        }

        public void EditClientInfo(string name, string surname, string phone, string email, HttpPostedFileBase image, string photoPath)
        {
            throw new NotImplementedException();
        }

        public Client EditClient(string name, string surname, string phone, string email, Gender gender, ClubCard card)
        {
            throw new NotImplementedException();
        }

        public void EditClientInfo(int clientId, string name, string surname, string phone, string email, HttpPostedFileBase image, string photoPath)
        {
            var client = GetClientById(clientId);

            if (client == null) return;

            client.Name = name;
            client.Surname = surname;
            client.PhoneNumber = phone;
            client.Email = email;
            if(image!=null)
                client.PhotoPath = WorkImage.SaveImage(image, photoPath, 400, 600);
            _context.SaveChanges();
        }

        public Client GetClientById(int clientId)
        {
            return _context.Clients.FirstOrDefault(x => x.Id == clientId);
        }

        public bool UpdateClient(string name, string surname, string secondname, string phonenumber, string email, string clubcardnumber)
        {
            var clientDb = _context.Clients.FirstOrDefault(x => x.Email == email);

            if (clientDb == null) return false;

            clientDb.Name = name;
            clientDb.Surname = surname;
            clientDb.SecondName = secondname;
            clientDb.PhoneNumber = phonenumber;
            clientDb.Email = email;
            clientDb.ClubCardId.ClubCardNumber = clubcardnumber;

            _context.SaveChanges();
            return true;
        }
    }
}
