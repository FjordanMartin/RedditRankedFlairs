using System.Security.Claims;

namespace RedditFlairs.Core.Extensions
{
    public static class PrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var nameId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(nameId, out var id) ? id : 0;
        }
    }
}