using Api_AIKO.Models.JwtConfiguration;

namespace Api_AIKO.Interfaces
{
    public interface IAuthenticationBusinessLogics
    {
        string GenerateToken(string username, JwtConfiguration jwtConfiguration);
    }
}

