using RentYourHome.Models.Addresses;

namespace RentYourHome.Models.Ads;

public class AdDto
{
    public AddressDto Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public IList<string> Images { get; init; }
}