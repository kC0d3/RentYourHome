namespace RentYourHome.Models;

public class UserAdApplication
{
    public Ad Ad { get; init; }
    public int AdId { get; init; }

    public User User { get; init; }
    public int UserId { get; init; }
}