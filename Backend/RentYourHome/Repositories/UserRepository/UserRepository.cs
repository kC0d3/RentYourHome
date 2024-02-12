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

    public async Task<User> GetUserByUserName(string userName)
    {
        await using var dbContext = new DatabaseContext();
        return dbContext.Users
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Images)
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Address)
            .FirstOrDefault(u => u.UserName == userName);
    }
    
    public async Task<User> GetUserById(int id)
    {
        await using var dbContext = new DatabaseContext();
        return dbContext.Users.FirstOrDefault(u => u.Id == id);
    }

    /*public void UpdateUser(User user)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Update(user);
        dbContext.SaveChanges();
    }*/
    
    public void DeleteUser(User user)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Remove(user);
        dbContext.SaveChanges();
    }
}