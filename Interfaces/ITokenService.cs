
using auth_api.Models;

namespace auth_api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}