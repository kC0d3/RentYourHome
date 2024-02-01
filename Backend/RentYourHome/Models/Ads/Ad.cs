using System.Text.Json.Serialization;
using RentYourHome.Models.Addresses;
using RentYourHome.Models.Images;
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
    public ICollection<Image> Images { get; init; }

    public int UserId { get; init; }
    public ICollection<UserAdApplication> UserAdApplications { get; init; }
}