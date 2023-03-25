using MS.Common.Enums;
using MS.DataLayer.Entities;
using System.Linq;
using System.Web;

namespace MS.DataLayer.Abstract
{
    public interface IUserRepository
    {
        User AddUser(string email, string password, string name, string surname, EUserTypes role);
        User EditUser(string email, string password, string name, string surname);
        User GetUserByEmail(string email);
        IQueryable<User> Users();
        Role GetRoleById(int id);
        IQueryable<Role> GetRoles();
        bool CheckUserPassword(string email, string password);
        bool CheckIsTrainer(string email);
        IQueryable<User> GetAllTrainers();
        User GetUserById(int userId);
        void EditUserInfo(int userId, string name, string surname, string phone, string email, HttpPostedFileBase image, string photoPath);
        User GetTrainerByEmail(string email);
        bool UpdateTrainer(string name, string surname, string phonenumber, string email);
        void RemoveTrainer(int id);
        User AddTrainer(string name, string surname, string phonenumber, string email, string rolename);
        bool CheckManagerPassword(string login, string pass);
    }
}