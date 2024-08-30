using APIs.DTOs;
using APIs.Models;

namespace APIs.Services
{
    public interface IJwtService
    {
        AthenticationResponseDTO CreateJwtToken(ApplicationUser user, IList<string> roles);
    }
}
