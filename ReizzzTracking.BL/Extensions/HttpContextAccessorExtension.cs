using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ReizzzTracking.BL.Errors.Auth;

namespace ReizzzTracking.BL.Extensions
{
    public static class HttpContextAccessorExtension
    {

        public static long GetCurrentUserIdFromJwt(this IHttpContextAccessor _httpContextAccessor)
        {
            string? currentUserIdString = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (currentUserIdString is null)
            {
                throw new Exception(AuthError.UserClaimsAccessFailed);
            }
            long currentUserId = long.Parse(currentUserIdString);
            return currentUserId;
        }
    }
}