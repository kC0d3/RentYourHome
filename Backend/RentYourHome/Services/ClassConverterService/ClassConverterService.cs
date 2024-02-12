using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;
using RentYourHome.Models.Images;
using RentYourHome.Models.Users;

namespace RentYourHome.Services.ClassConverterService;

public class ClassConverterService : IClassConverterService
{
    //DTO to DB class converters.
    public User UserReqDtoToUser(UserReqDto user)
    {
        return new User
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };
    }

    public Ad AdReqDtoToAd(AdReqDto ad)
    {
        return new Ad
        {
            Address = AddressDtoToAddress(ad.Address),
            Rooms = ad.Rooms,
            Size = ad.Size,
            Price = ad.Price,
            Description = ad.Description,
            Images = StringsToImages(ad.Images),
            UserId = ad.UserId
        };
    }

    //DB to DTO class converters.
    public ICollection<AdDto> AdsToAdDtos(IEnumerable<Ad> ads)
    {
        return ads.Select(ad => new AdDto
        {
            Id = ad.Id,
            Address = AddressToAddressDto(ad.Address),
            Rooms = ad.Rooms,
            Size = ad.Size,
            Price = ad.Price,
            Description = ad.Description,
            Approved = ad.Approved,
            Images = ImagesToStrings(ad.Images),
        }).ToList();
    }

    public AdDto AdToAdDto(Ad ad)
    {
        return new AdDto
        {
            Id = ad.Id,
            Address = AddressToAddressDto(ad.Address),
            Rooms = ad.Rooms,
            Size = ad.Size,
            Price = ad.Price,
            Description = ad.Description,
            Approved = ad.Approved,
            Images = ImagesToStrings(ad.Images),
        };
    }

    public UserDto UserToUserDto(User user)
    {
        return new UserDto
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Accepted = user.Accepted,
            PublishedAds = AdsToAdDtos(user.PublishedAds)
        };
    }

    //Local converters.
    private static ICollection<string> ImagesToStrings(IEnumerable<Image> images)
    {
        return images.Select(image => new string(image.ImageName)).ToList();
    }

    private static ICollection<Image> StringsToImages(IEnumerable<string> strings)
    {
        return strings.Select(str => new Image
        {
            ImageName = str
        }).ToList();
    }

    private static Address AddressDtoToAddress(AddressDto addressDto)
    {
        return new Address
        {
            ZipCode = addressDto.ZipCode,
            City = addressDto.City,
            Street = addressDto.Street,
            HouseNumber = addressDto.HouseNumber
        };
    }

    private static AddressDto AddressToAddressDto(Address address)
    {
        return new AddressDto
        {
            ZipCode = address.ZipCode,
            City = address.City,
            Street = address.Street,
            HouseNumber = address.HouseNumber
        };
    }
}