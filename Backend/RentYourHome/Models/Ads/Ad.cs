using RentYourHome.Models.Addresses;
using RentYourHome.Models.Images;
using RentYourHome.Models.UserAdApplications;
using RentYourHome.Models.Users;

namespace RentYourHome.Models.Ads;

public class Ad
{
    public int Id { get; init; }
    public Address Address { get; set; }
    public int Rooms { get; set; }
    public int Size { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public bool Approved { get; set; }
    public ICollection<Image>? Images { get; init; }


    public User User { get; init; }
    public int UserId { get; init; }
    public ICollection<UserAdApplication> UserAdApplications { get; set; }
}