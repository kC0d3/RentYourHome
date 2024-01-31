using RentYourHome.Models.Addresses;
using RentYourHome.Models.Images;

namespace RentYourHome.Models.Ads;

public class AdDto
{
    public AddressDto Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public ICollection<Image> Images { get; init; }
}