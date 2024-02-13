using RentYourHome.Models.Ads;
using RentYourHome.Models.UserAdApplications;

namespace RentYourHome.Models.Users;

public class User
{
    public int Id { get; init; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Accepted { get; set; }
    public ICollection<Ad> PublishedAds { get; init; }
    
    public ICollection<UserAdApplication> UserAdApplications { get; init; }
}