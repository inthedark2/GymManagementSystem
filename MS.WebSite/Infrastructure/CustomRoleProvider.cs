using MS.Common.Constans;
using MS.DataLayer.Entities;
using System;
using System.Linq;
using System.Web.Security;

namespace MS.WebSite.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly ManagmentSystemContext _context;
        public CustomRoleProvider()
        {
            _context = new ManagmentSystemContext();
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (_context.Clients.FirstOrDefault(x => x.Email == username) != null)
            {
                return new string[] { Constants.Client };
            }
            if (_context.Users.FirstOrDefault(x => x.Email == username) != null)
            {
                return new string[] { _context.Users.FirstOrDefault(x => x.Email == username)?.Role.RoleName };
            }
            else
                return new string[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == username);
            var client = _context.Clients.SingleOrDefault(x => x.Email == username);
            if (user == null && client == null)
                return false;
            if (user.Role.RoleName == roleName)
                return true;
            if (user == null && roleName == Constants.Client)
                return true;
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}