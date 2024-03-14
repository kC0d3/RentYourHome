using Microsoft.EntityFrameworkCore;
using RentYourHome.Data;
using RentYourHome.Models.UserAdApplications;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Repositories.UserAdApplicationRepository;

public class UserAdApplicationRepository : IUserAdApplicationRepository
{
    private readonly DatabaseContext _dbContext;

    public UserAdApplicationRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserAdApplication?> GetUserAdApplication(int adId, int userId)
    {
        return await _dbContext.UserAdApplications.FirstOrDefaultAsync(ua => ua.AdId == adId && ua.UserId == userId);
    }

    public void DeleteUserAdApplication(UserAdApplication userAdApplication)
    {
        _dbContext.Remove(userAdApplication);
        _dbContext.SaveChanges();
    }
}