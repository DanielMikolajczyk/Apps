using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Authorization
{
    public class IsShadowBannedHandler : AuthorizationHandler<IsShadowBannedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsShadowBannedRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == "ShadowBanned"))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
