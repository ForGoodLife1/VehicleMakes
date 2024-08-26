using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using VehicleMakes.authorization;
using VehicleMakes;
using VehicleMakes.Model.Model;

namespace VehicleMakes.authorization
{
    public class PermissionBasedAuthorizationFilter(VehicleMakesDbContext dbContext) : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var attribute = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute) as CheckPermissionAttribute;
            if (attribute != null)
            {
                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                    return;
                }

                if (!int.TryParse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
                {
                    context.Result = new UnauthorizedResult(); // Invalid user ID
                    return;
                }

                var requiredPermission = attribute.Permission;
                var hasPermission = await dbContext.UserPermissions.AnyAsync(x => x.UserId == userId && x.PermissionId == requiredPermission);
                if (!hasPermission)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }

}
/*public class PermissionBasedauthorizationFilter(VehicleMakesDbContext dbContext) : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var atribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckPermissionAttribute);
        if (atribute != null)
        {
            var claimIdentity = context.HttpContext.User.Identities as ClaimsIdentity;
            if (ClaimsIdentity ==null||!ClaimsIdentity.IsAuthenticated)
            {
                context.result =new ForbidResult();
            }
            else
            {
                var userId = int parse(claimIdentity.FindFirst(claimIdentity.NameIdentifier).value);
                var haspermission = dbContext.Set<UserPermission>().Any(x => x.UserId==userId&&x.PermissionId==atribute.Permission);
                if (!haspermission)
                {
                    context.Result =new ForbidResult();
                }
            }

        }
    }
}*/