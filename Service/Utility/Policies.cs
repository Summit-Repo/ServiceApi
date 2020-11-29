using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Utility
{
    public class Policies
    {
        
            public const string Admin = "Admin";
            public const string User = "User";
            public static AuthorizationPolicy AdminPolicy()
            {
                 return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).RequireClaim("create","1").RequireClaim("delete","1").Build();
            }
            public static AuthorizationPolicy UserPolicy()
            {
                return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
            }
    }
}

