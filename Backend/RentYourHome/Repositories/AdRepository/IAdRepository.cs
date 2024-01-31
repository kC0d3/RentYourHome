using RentYourHome.Models;

namespace RentYourHome.Repositories.AdRepository;

public interface IAdRepository
{
    void AddAdToDb(AdDto ad);
}