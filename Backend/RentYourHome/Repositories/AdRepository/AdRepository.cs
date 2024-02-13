using Microsoft.EntityFrameworkCore;
using RentYourHome.Data;
using RentYourHome.Models.Ads;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Repositories.AdRepository;

public class AdRepository : IAdRepository
{
    private readonly IClassConverterService _classConverterService;

    public AdRepository(IClassConverterService classConverterService)
    {
        _classConverterService = classConverterService;
    }

    public void AddAdToDb(AdReqDto ad)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Add(_classConverterService.AdReqDtoToAd(ad));
        dbContext.SaveChanges();
    }

    public async Task<IEnumerable<AdDto>> GetAllAds()
    {
        await using var dbContext = new DatabaseContext();
        return _classConverterService.AdsToAdDtos(dbContext.Ads
            .Include(a => a.Images)
            .Include(a => a.Address).ToList());
    }

    public async Task<Ad?> GetAdById(int id)
    {
        await using var dbContext = new DatabaseContext();
        return dbContext.Ads.Include(a => a.Address)
            .Include(a => a.Images)
            .FirstOrDefault(a => a.Id == id);
    }

    public void UpdateAd(Ad ad)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Update(ad);
        dbContext.SaveChanges();
    }

    public void DeleteAd(Ad ad)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Remove(ad);
        dbContext.SaveChanges();
    }
}