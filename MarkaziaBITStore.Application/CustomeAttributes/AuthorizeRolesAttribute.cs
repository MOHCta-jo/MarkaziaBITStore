using MarkaziaBITStore.Application.Enums.AppRoles;
using Microsoft.AspNetCore.Authorization;


//Usage in controller or action :
//[AuthorizeRoles(AppRole.Admin, AppRole.Manager)]
//new Claim(ClaimTypes.Role, AppRole.Admin.ToString()) // "Admin" Added to JWT Claims

namespace MarkaziaBITStore.Application.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params AppRole[] roles)
        {
            if (roles?.Any() == true)
            {
                Roles = string.Join(",", roles.Select(r => r.ToString()));
            }
        }
    }

}
