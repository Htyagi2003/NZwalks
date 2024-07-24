using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Repositories
{
    public interface ITokenRepo
    {
        string CreateJwtToken(IdentityUser user,List<string> roles);
    }
}
