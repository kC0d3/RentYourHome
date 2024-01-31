using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;
using RentYourHome.Models.Users;

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
            Address = new Address
            {
                ZipCode = user.Address.ZipCode,
                City = user.Address.City,
                Street = user.Address.Street,
                HouseNumber = user.Address.HouseNumber
            }
        };
    }

    public Ad ConvertToDbClass(AdDto ad)
    {
        return new Ad
        {
            Address = new Address
            {
                ZipCode = ad.Address.ZipCode,
                City = ad.Address.City,
                Street = ad.Address.Street,
                HouseNumber = ad.Address.HouseNumber
            },
            Rooms = ad.Rooms,
            Size = ad.Size,
            Price = ad.Price,
            Description = ad.Description,
            Images = ad.Images
        };
    }
}