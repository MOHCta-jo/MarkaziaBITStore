using MarkaziaBITStore.Application.Contracts.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Services.User
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            //TODO : for now we are assuming the user is authenticated and the token contains a "sub" claim with the user ID.

            //var user = _httpContextAccessor.HttpContext?.User;
            //if (user == null || !user.Identity?.IsAuthenticated == true)
            //    throw new UnauthorizedAccessException("User is not authenticated.");

            //var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var userId = "1";

            //if (string.IsNullOrEmpty(userId))
            //    throw new UnauthorizedAccessException("User ID claim not found in token.");

            return int.Parse(userId);
        }
    }

}
