using VehicleMakes.Model.Model;

namespace VehicleMakes.authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CheckPermissionAttribute : Attribute
    {
  
        public CheckPermissionAttribute(Permission permission)
        {
            Permission=permission;
        }

        public Permission Permission { get; }
    }
}
