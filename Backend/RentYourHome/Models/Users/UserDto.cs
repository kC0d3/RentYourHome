using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;

namespace RentYourHome.Models.Users;

public class UserDto
{
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public bool Accepted { get; set; }
    public ICollection<AdDto> PublishedAds { get; init; }
    
}