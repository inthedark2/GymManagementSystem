using System.Linq;
using MS.Common.Enums;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.IServices
{
    public interface IClientService
    {
        Client AddClient(string name, string surname, string secondname, string phone, string email, Gender gender);
        Client AddClient(string name, string surname, string secondname, string phone, string email, EGenderType genderType);
        //Client EditClient(string name, string surname, string phone, string email, Gender gender, ClubCard card);
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
        bool UpdateClient(string name, string surname, string secondname, string phonenumber, string email, string clubcardnumber);
    }
}
