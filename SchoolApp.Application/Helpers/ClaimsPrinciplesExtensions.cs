using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(value, out var userId))
            return userId;
        return null;
    }
}
