using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.Authorization
{
    public class IsExpert : IAuthorizationRequirement
    {
        public IsExpert()
        {

        }
    }
}
