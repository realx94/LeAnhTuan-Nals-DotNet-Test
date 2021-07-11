using Core.Entities;
using System.Security.Claims;

namespace Core.Services
{
    public interface IAuthenticationManager
    {
        string GenerateToken(User user, int expireMinutes = 60, params Claim[] claims);
        ClaimsPrincipal ValidateToken(string jwtToken);
    }
}
