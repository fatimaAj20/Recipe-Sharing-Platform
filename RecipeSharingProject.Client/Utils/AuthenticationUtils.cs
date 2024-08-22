using System.Security.Claims;

namespace RecipeSharingProject.Client.Utils
{
    public static class AuthenticationUtils
    {
        public static string GetEmail(ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        }
    }
}
