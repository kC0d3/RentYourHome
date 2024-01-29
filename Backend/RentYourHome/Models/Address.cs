namespace RentYourHome.Models;

public class Address
{
    public string ZipCode { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public int HouseNumber { get; init; }
    public int Floor { get; init; }
    public int Door { get; init; }
}