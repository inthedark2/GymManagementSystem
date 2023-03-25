using MS.DataLayer.Abstract;
using System.Linq;
using MS.DataLayer.Entities;
using MS.Common.Enums;
using System;
using System.Web;
using MS.Common.Services;
using System.Configuration;

namespace MS.DataLayer.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ManagmentSystemContext _context;
        public UserRepository(ManagmentSystemContext context)
        {
            this._context = context;
        }
        public User AddUser(string email, string password, string name, string surname, EUserTypes typeId)
        {
            User user = GetUserByEmail(email);
            if (user == null)
            {
                user = new User();
                var crypto = new SimpleCrypto.PBKDF2();
                user.Password = crypto.Compute(password);
                user.PasswordSalt = crypto.Salt;
                user.Email = email;
                user.Name = name;
                user.Surname = surname;
                if (GetRoleById((int)typeId) == null)
                {
                    return null;
                }
                user.Role = GetRoleById((int)typeId);
                user.PhotoPath = ConfigurationManager.AppSettings["DefaultImageName"];
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            else
                return null;
        }

        public bool CheckIsTrainer(string email)
        {
            User user = GetUserByEmail(email);
            if (user != null && user.Role.Id == (int)EUserTypes.Trainer)
            {
                return true;
            }
            return false;
        }

        public bool CheckUserPassword(string email, string password)
        {
            User user = GetUserByEmail(email);
            if (user != null)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    return true;
            }
            return false;
        }

        public User EditUser(string email, string password, string name, string surname)
        {
            User user = GetUserByEmail(email);
            if (user != null)
            {
                user.Name = name;
                user.Surname = surname;
                var crypto = new SimpleCrypto.PBKDF2();
                user.Password = crypto.Compute(password);
                user.PasswordSalt = crypto.Salt;
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public void EditUserInfo(int userId, string name, string surname, string phone, string email, HttpPostedFileBase Image, string photoPath)
        {
            User user = GetUserById(userId);
            if (user != null)
            {
                user.Name = name;
                user.Surname = surname;
                user.PhoneNumber = phone;
                user.Email = email;
                if (Image != null)
                    user.PhotoPath = WorkImage.SaveImage(Image, photoPath, 400, 600);
                _context.SaveChanges();
            }
        }

        public IQueryable<User> GetAllTrainers()
        {
            return _context.Users.Where(x => x.Role.Id == (int)EUserTypes.Trainer);
        }

        public User GetTrainerByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Role.Id == (int)EUserTypes.Trainer && x.Email == email);
        }

        public Role GetRoleById(int id)
        {
            return this.GetRoles().SingleOrDefault(r => r.Id == id);
        }

        public IQueryable<Role> GetRoles()
        {
            return from data in _context.UserRoles select data;
        }

        public User GetUserByEmail(string email)
        {
            return this.Users().SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }

        public IQueryable<User> Users()
        {
            return from data in _context.Users select data;
        }

        public bool UpdateTrainer(string name, string surname, string phonenumber, string email)
        {
            var trainerDb = GetTrainerByEmail(email);

            if (trainerDb != null)
            {
                trainerDb.Email = email;
                trainerDb.Name = name;
                trainerDb.Surname = surname;
                trainerDb.PhoneNumber = phonenumber;

                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public void RemoveTrainer(int id)
        {
            var trainer = GetUserById(id);

            if (trainer.Role.Id == (int)EUserTypes.Trainer)
            {
                _context.Users.Remove(trainer);
                _context.SaveChanges();
            }
        }

        public User AddTrainer(string name, string surname, string phonenumber, string email, string rolename)
        {
            _context.Users.Add(new User
            {
                Name = name,
                Surname = surname,
                Email = email,
                PhoneNumber = phonenumber,
                Role = GetRoleById((int)EUserTypes.Trainer),
                Password = @"Yn5wul1ZPyJBOvctl7CkHu2h / pvCwb2Ao7er1TiLcn9kwKUSAbQuIW / Dhd7eckYOUDGlgjWGF48o4iIwcas + pw ==",
                PasswordSalt = @"100000.MNnRJi7vjGGNMdSKtiEs+rEA6NS7166TYM14pgk5ivwUXQ=="
            });

            _context.SaveChanges();

            var trainerUser = GetTrainerByEmail(email);
            if (trainerUser != null)  return trainerUser;

            return null;
        }

        public bool CheckManagerPassword(string login, string pass)
        {
            User user = GetUserByEmail(login);
            if (user == null || user.Role.Id != (int) EUserTypes.Manager) return false;

            var crypto = new SimpleCrypto.PBKDF2();
            return user.Password == crypto.Compute(pass, user.PasswordSalt);
        }
    }
}
