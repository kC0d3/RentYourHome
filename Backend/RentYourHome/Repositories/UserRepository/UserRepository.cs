using Microsoft.EntityFrameworkCore;
using RentYourHome.Data;
using RentYourHome.Models.Users;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly IClassConverterService _classConverterService;
    private readonly DatabaseContext _dbContext;

    public UserRepository(DatabaseContext dbContext, IClassConverterService classConverterService)
    {
        _classConverterService = classConverterService;
        _dbContext = dbContext;
    }

    public void AddUserToDb(UserReqDto user)
    {
        _dbContext.Add(_classConverterService.UserReqDtoToUser(user));
        _dbContext.SaveChanges();
    }

    public async Task<User?> GetUserByUserName(string username)
    {
        return _dbContext.Users
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Images)
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Address)
            .FirstOrDefault(u => u.Username == username);
    }

    public async Task<User?> GetUserById(int id)
    {
        return _dbContext.Users
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Images)
            .Include(u => u.PublishedAds)
            .ThenInclude(a => a.Address)
            .FirstOrDefault(u => u.Id == id);
    }

    public void UpdateUser(User user)
    {
        _dbContext.Update(user);
        _dbContext.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        _dbContext.Remove(user);
        _dbContext.SaveChanges();
    }
}