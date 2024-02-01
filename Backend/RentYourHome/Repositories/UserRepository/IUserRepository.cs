using RentYourHome.Models.Users;

namespace RentYourHome.Repositories.UserRepository;

public interface IUserRepository
{
    void AddUserToDb(UserReqDto user);
    UserDto GetUserByUserName(string userName);
}