using System.Linq;
using MS.Common.Enums;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.IServices
{
    public interface IUserService
    {
        User AddUser(string email, string password, string name, string surname, EUserTypes role);
        User EditUser(string email, string password, string name, string surname);
        User GetUserByEmail(string email);
        IQueryable<User> Users();
        Role GetRoleById(int id);
        IQueryable<Role> GetRoles();
        bool CheckUserPassword(string email, string password);
        bool CheckIsTrainer(string email);
        User GetTrainerByEmail(string email);
        IQueryable<User> GetAllTrainers();
        bool UpdateTrainer(string name, string surname, string phonenumber, string email);
        void RemoveTrainer(int id);
        User AddTrainer(string name, string surname, string phonenumber, string email, string rolename);
        bool CheckManagerPassword(string login, string pass);
    }
}
