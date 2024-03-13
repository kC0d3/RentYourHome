using RentYourHome.Models.UserAdApplications;

namespace RentYourHome.Repositories.UserAdApplicationRepository;

public interface IUserAdApplicationRepository
{
    Task<UserAdApplication?> GetUserAdApplication(int adId, int userId);
    void DeleteUserAdApplication(UserAdApplication userAdApplication);
}