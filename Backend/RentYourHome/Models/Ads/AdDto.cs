using RentYourHome.Models.Addresses;
using RentYourHome.Models.UserAdApplications;

namespace RentYourHome.Models.Ads;

public class AdDto
{
    public int Id { get; init; }
    public AddressDto Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public bool Approved { get; init; }
    public string Description { get; init; }
    public ICollection<string> Images { get; init; }
    public ICollection<UserAdApplicationDto> UserAdApplications { get; init; }
}