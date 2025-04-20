using authentication_api.Domain.Entities;

namespace authentication_api.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
