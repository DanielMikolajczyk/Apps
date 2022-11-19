using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Authorization
{
    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == "Admin"))
            {
                context.Succeed(requirement); 
            }
            return Task.CompletedTask;
        }
    }
}
