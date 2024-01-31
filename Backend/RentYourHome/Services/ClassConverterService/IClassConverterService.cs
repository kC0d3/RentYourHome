using RentYourHome.Models;

namespace RentYourHome.Services.ClassConverterService;

public interface IClassConverterService
{
    User ConvertToDbClass(UserDto user);
    Ad ConvertToDbClass(AdDto ad);
}