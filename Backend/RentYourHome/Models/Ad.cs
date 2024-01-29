using System.Net.Mime;

namespace RentYourHome.Models;

public class Ad
{
    public int Id { get; init; }
    public Address Address { get; init; }
    public int Rooms { get; init; }
    public int Size { get; init; }
    public int Price { get; init; }
    public string Description { get; init; }
    public ICollection<Image> Images { get; init; }
    public User PostedBy { get; init; }

    public int PostedByUserId { get; init; }
    public ICollection<User> AplliedUsers { get; init; }
}