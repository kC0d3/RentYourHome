using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RentYourHome.Models.Ads;
using RentYourHome.Repositories.AdRepository;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Controllers;

[ApiController]
[Route("api/ads")]
public class AdController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IAdRepository _adRepository;
    private readonly IClassConverterService _classConverterService;

    public AdController(ILogger<UserController> logger, IAdRepository adRepository,
        IClassConverterService classConverterService)
    {
        _logger = logger;
        _adRepository = adRepository;
        _classConverterService = classConverterService;
    }

    [HttpPost]
    public ActionResult<AdReqDto> CreateAd([Required] AdReqDto ad)
    {
        try
        {
            _adRepository.AddAdToDb(ad);
            return Ok(ad);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<AdDto>>> GetAllAds()
    {
        try
        {
            return Ok(await _adRepository.GetAllAds());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdDto>> GetAdById(int id)
    {
        try
        {
            var ad = await _adRepository.GetAdById(id);
            return Ok(_classConverterService.AdToAdDto(ad));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AdDto>> DeleteAdById(int id)
    {
        try
        {
            var ad = await _adRepository.GetAdById(id);
            _adRepository.DeleteAd(ad);
            return Ok(_classConverterService.AdToAdDto(ad));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }
}