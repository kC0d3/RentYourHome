using RentYourHome.Models.Users;

namespace RentYourHome.Repositories.UserRepository;

public interface IUserRepository
{
    void AddUserToDb(UserReqDto user);
    Task<User?> GetUserByUserName(string userName);
    Task<User?> GetUserById(int id);
    void UpdateUser(User user);
    void DeleteUser(User user);
}