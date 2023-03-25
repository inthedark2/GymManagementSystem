using MS.Common.Enums;
using MS.DataLayer.Entities;
using System.Linq;
using System.Web;

namespace MS.DataLayer.Abstract
{
    public interface IClientRepository : IRepository
    {
        Client AddClient(string name, string surname, string secondname, string phone, string email, Gender gender);
        Client AddClient(string name, string surname, string secondname, string phone, string email, EGenderType genderType);
        Client GetClientByEmail(string email);
        Client GetClientByEmailAndCard(string email, string card);
        Client GetClientByClubCard(ClubCard card);
        bool RemoveClientByClubCard(ClubCard card);
        IQueryable<Client> Clients();
        bool AddCardToClient(ClubCard card);
        Client RegisterClient(string email, string password);
        bool CheckClientPassword(string email, string password);
        bool SetVerificationCode(string email, int code);
        int GetUserType(string email);
        void EditClientInfo(int clientId,string name, string surname, string phone, string email, HttpPostedFileBase Image, string photoPath);
        Client GetClientById(int clientId);
        bool UpdateClient(string name, string surname, string secondname, string phonenumber, string email, string clubcardnumber);
    }
}
