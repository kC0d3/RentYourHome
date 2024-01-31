using RentYourHome.Models.Addresses;
using RentYourHome.Models.UserAdApplications;

namespace RentYourHome.Models.Ads;

public class Ad
{
    public int Id { get; init; }
    public Address Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public IList<string> Images { get; init; }

    public ICollection<UserAdApplication> UserAdApplications { get; init; }
}