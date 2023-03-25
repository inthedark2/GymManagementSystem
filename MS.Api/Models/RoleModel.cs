using System.Collections.Generic;

namespace MS.Api.Models
{
    public sealed class RoleModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserModel> Users { get; set; }

        public RoleModel()
        {
            Users = new List<UserModel>();
        }
    }
}