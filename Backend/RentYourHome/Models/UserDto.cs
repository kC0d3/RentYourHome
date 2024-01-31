namespace RentYourHome.Models;

public class UserDto
{
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public Address Address { get; init; }
}