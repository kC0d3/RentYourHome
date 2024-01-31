using RentYourHome.Models.Ads;
using RentYourHome.Models.Users;

namespace RentYourHome.Services.ClassConverterService;

public interface IClassConverterService
{
    User ConvertToDbClass(UserDto user);
    Ad ConvertToDbClass(AdDto ad);
}