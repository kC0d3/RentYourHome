using RentYourHome.Models.Ads;
using RentYourHome.Models.UserAdApplications;

namespace RentYourHome.Models.Users;

public class UserDto
{
    public int Id { get; init; }
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public bool Accepted { get; set; }
    public ICollection<AdDto> PublishedAds { get; init; }
    public ICollection<UserAdApplicationDto> UserAdApplications { get; init; }
}