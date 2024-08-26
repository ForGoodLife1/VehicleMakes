using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleMakes.Model.Model
{
    public class UserPermission
    {
        public int UserId { get; set; }
        public Permission PermissionId { get; set; }
    }
}
