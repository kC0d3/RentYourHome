using System.Text.Json.Serialization;
using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;
using RentYourHome.Models.UserAdApplications;

namespace RentYourHome.Models.Users;

public class User
{
    public int Id { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public bool Accepted { get; set; }

    public ICollection<Ad> PublishedAds { get; init; }
    public ICollection<UserAdApplication> UserAdApplications { get; init; }
}