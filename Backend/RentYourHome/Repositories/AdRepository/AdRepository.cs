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

    public IEnumerable<AdDto> GetAllAds()
    {
        using var dbContext = new DatabaseContext();
        return _classConverterService.AdsToAdDtos(dbContext.Ads
            .Include(a => a.Images)
            .Include(a => a.Address).ToList());
    }
}