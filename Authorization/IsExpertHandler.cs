using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Authorization
{
    public class IsExpertHandler : AuthorizationHandler<IsExpert>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsExpert requirement)
        {
            if(context.User.HasClaim(c => c.Type == "Expert"))
            {
                context.Succeed(requirement); 
            }
            return Task.CompletedTask;
        }


    }
}
