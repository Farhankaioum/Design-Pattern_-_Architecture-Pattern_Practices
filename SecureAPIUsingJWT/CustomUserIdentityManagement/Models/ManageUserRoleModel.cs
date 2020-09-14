using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomUserIdentityManagement.Models
{
    public class ManageUserRoleModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
