using RentYourHome.Models.Ads;

namespace RentYourHome.Repositories.AdRepository;

public interface IAdRepository
{
    void AddAdToDb(AdDto ad);
}