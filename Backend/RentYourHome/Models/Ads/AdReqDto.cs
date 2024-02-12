using RentYourHome.Models.Addresses;

namespace RentYourHome.Models.Ads;

public class AdReqDto
{
    public AddressDto Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public bool Approved { get; init; }
    public ICollection<string> Images { get; init; }
    public int UserId { get; init; }
}