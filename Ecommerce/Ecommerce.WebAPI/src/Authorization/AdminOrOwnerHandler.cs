using Ecommerce.Core.src.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Ecommerce.WebAPI.src.Authorization
{
    public class AdminOrOwnerHandler :
   AuthorizationHandler<AdminOrOwnerRequirement, Order>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrOwnerRequirement requirement, Order orderResource)
        {
            var user = context.User;
            var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
            var userID = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

            if (userRole == Role.Admin.ToString())
            {
                context.Succeed(requirement);
            }

            if (userID == orderResource.UserId.ToString())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class AdminOrOwnerRequirement : IAuthorizationRequirement { }
}
