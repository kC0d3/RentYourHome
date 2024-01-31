using RentYourHome.Models;

namespace RentYourHome.Services.ClassConverterService;

public class ClassConverterService : IClassConverterService
{
    public User ConvertToDbClass(UserDto user)
    {
        return new User
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address
        };
    }

    public Ad ConvertToDbClass(AdDto ad)
    {
        return new Ad
        {
            Address = ad.Address,
            Rooms = ad.Rooms,
            Size = ad.Size,
            Price = ad.Price,
            Description = ad.Description,
            Images = ad.Images
        };
    }
}