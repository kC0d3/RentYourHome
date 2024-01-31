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

    public void AddUserToDb(UserDto user)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Add(_classConverterService.ConvertToDbClass(user));
        dbContext.SaveChanges();
    }
}