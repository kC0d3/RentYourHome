using Microsoft.EntityFrameworkCore;
using RentYourHome.Data;
using RentYourHome.Models.Ads;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Repositories.AdRepository;

public class AdRepository : IAdRepository
{
    private readonly IClassConverterService _classConverterService;
    private readonly DatabaseContext _dbContext;

    public AdRepository(DatabaseContext dbContext, IClassConverterService classConverterService)
    {
        _classConverterService = classConverterService;
        _dbContext = dbContext;
    }

    public void AddAdToDb(AdReqDto ad)
    {
        _dbContext.Add(_classConverterService.AdReqDtoToAd(ad));
        _dbContext.SaveChanges();
    }

    public async Task<IEnumerable<AdDto>> GetAllAds()
    {
        return _classConverterService.AdsToAdDtos(_dbContext.Ads
            .Include(a => a.Images)
            .Include(a => a.Address).ToList());
    }

    public async Task<Ad?> GetAdById(int id)
    {
        return _dbContext.Ads
            .Include(a => a.Address)
            .Include(a => a.UserAdApplications)
            .Include(a => a.Images)
            .FirstOrDefault(a => a.Id == id);
    }

    public void UpdateAd(Ad ad)
    {
        _dbContext.Update(ad);
        _dbContext.SaveChanges();
    }

    public void DeleteAd(Ad ad)
    {
        _dbContext.Remove(ad);
        _dbContext.SaveChanges();
    }
}