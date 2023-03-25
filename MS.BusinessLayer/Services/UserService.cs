using System.Linq;
using MS.BusinessLayer.IServices;
using MS.Common.Enums;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User AddUser(string email, string password, string name, string surname, EUserTypes role)
        {
            return _userRepository.AddUser(email, password, name, surname, role);
        }

        public User EditUser(string email, string password, string name, string surname)
        {
            return _userRepository.EditUser(email, password, name, surname);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public IQueryable<User> Users()
        {
            return _userRepository.Users();
        }

        public Role GetRoleById(int id)
        {
            return _userRepository.GetRoleById(id);
        }

        public bool CheckManagerPassword(string login, string pass)
        {
            return _userRepository.CheckManagerPassword(login, pass);
        }

        public IQueryable<Role> GetRoles()
        {
            return _userRepository.GetRoles();
        }

        public bool CheckUserPassword(string email, string password)
        {
            return _userRepository.CheckUserPassword(email, password);
        }

        public bool CheckIsTrainer(string email)
        {
            return _userRepository.CheckIsTrainer(email);
        }

        public User GetTrainerByEmail(string email)
        {
            return _userRepository.GetTrainerByEmail(email);
        }

        public IQueryable<User> GetAllTrainers()
        {
            return _userRepository.GetAllTrainers();
        }

        public bool UpdateTrainer(string name, string surname, string phonenumber, string email)
        {
            return _userRepository.UpdateTrainer(name, surname, phonenumber, email);
        }

        public void RemoveTrainer(int id)
        {
            _userRepository.RemoveTrainer(id);
        }

        public User AddTrainer(string name, string surname, string phonenumber, string email, string rolename)
        {
            return _userRepository.AddTrainer(name, surname, phonenumber, email, rolename);
        }
    }
}
