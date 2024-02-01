namespace RentYourHome.Models.Addresses;

public class Address
{
    public int Id { get; init; }
    public string ZipCode { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string HouseNumber { get; init; }
}