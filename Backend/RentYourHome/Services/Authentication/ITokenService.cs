using Microsoft.AspNetCore.Identity;

namespace RentYourHome.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}