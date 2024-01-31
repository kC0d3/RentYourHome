using RentYourHome.Models;

namespace RentYourHome.Repositories.UserRepository;

public interface IUserRepository
{
    void AddUserToDb(UserDto user);
}