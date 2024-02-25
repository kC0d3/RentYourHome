using RentYourHome.Contracts;
using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;
using RentYourHome.Models.Users;

namespace RentYourHome.Services.ClassConverterService;

public interface IClassConverterService
{
    User UserReqDtoToUser(UserReqDto user);
    Ad AdReqDtoToAd(AdReqDto ad);
    ICollection<AdDto> AdsToAdDtos(IEnumerable<Ad> ads);
    UserDto UserToUserDto(User user);
    AdDto AdToAdDto(Ad ad);

    UserReqDto RegistrationRequestToToUser(RegistrationRequest request);
}