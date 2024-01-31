using RentYourHome.Data;
using RentYourHome.Models;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Repositories.AdRepository;

public class AdRepository : IAdRepository
{
    private readonly IClassConverterService _classConverterService;

    public AdRepository(IClassConverterService classConverterService)
    {
        _classConverterService = classConverterService;
    }

    public void AddAdToDb(AdDto ad)
    {
        using var dbContext = new DatabaseContext();
        dbContext.Add(_classConverterService.ConvertToDbClass(ad));
        dbContext.SaveChanges();
    }
}