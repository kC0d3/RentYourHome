using RentYourHome.Models.Ads;

namespace RentYourHome.Models.Addresses;

public class Address
{
    public int Id { get; init; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }

    public Ad Ad { get; init; }
    public int AdId { get; init; }
}