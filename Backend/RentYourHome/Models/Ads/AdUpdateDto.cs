using RentYourHome.Models.Addresses;

namespace RentYourHome.Models.Ads;

public class AdUpdateDto
{
    public AddressDto Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public bool Approved { get; init; }
    public string Description { get; init; }
}