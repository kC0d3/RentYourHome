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
            Address = ConvertAddressDtoToAddress(user.Address)
        };
    }

    public Ad ConvertToDbClass(AdDto ad)
    {
        return new Ad
        {
            Address = ConvertAddressDtoToAddress(ad.Address),
            Rooms = ad.Rooms,
            Size = ad.Size,
            Price = ad.Price,
            Description = ad.Description,
            Images = ad.Images
        };
    }

    private static Address ConvertAddressDtoToAddress(AddressDto addressDto)
    {
        return new Address
        {
            ZipCode = addressDto.ZipCode,
            City = addressDto.City,
            Street = addressDto.Street,
            HouseNumber = addressDto.HouseNumber
        };
    }
}