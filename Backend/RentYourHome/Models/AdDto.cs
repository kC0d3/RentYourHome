namespace RentYourHome.Models;

public class AdDto
{
    public Address Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public ICollection<Image> Images { get; init; }
}