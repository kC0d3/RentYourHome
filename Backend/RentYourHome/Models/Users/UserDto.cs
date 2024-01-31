using RentYourHome.Models.Addresses;

namespace RentYourHome.Models.Users;

public class UserDto
{
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public AddressDto Address { get; init; }
}