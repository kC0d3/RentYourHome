using Microsoft.EntityFrameworkCore;
using RentYourHome.Data;
using RentYourHome.Models.Users;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly IClassConverterService _classConverterService;

    public UserRepository(IClassConverterService classConverterService)
    {
        _classConverterService = classConverterService;
    }

    public void AddUserToDb(UserReqDto user)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Add(_classConverterService.UserReqDtoToUser(user));
        dbContext.SaveChanges();
    }

    public UserDto GetUserByUserName(string userName)
    {
        using var dbContext = new DatabaseContext();
        return _classConverterService.UserToUserDto(dbContext.Users
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Images)
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Address)
            .FirstOrDefault(u => u.UserName == userName));
    }
}