using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomUserIdentityManagement.Models
{
    public class ManageUserRoleViewModel
    {
        public List<ManageUserRoleModel> manageUserRoleModels { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
