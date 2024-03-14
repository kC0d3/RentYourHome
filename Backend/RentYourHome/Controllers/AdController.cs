using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public async Task<ActionResult<ICollection<AdDto>>> GetAllAds()
    {
        try
        {
            return Ok(_classConverterService.AdsToAdDtos(await _adRepository.GetAllAds()));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting ads data.");
            return NotFound("Error getting ads data.");
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

    [Authorize(Roles = "User")]
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
            _logger.LogError(e, "Error create ad.");
            return BadRequest("Error create ad.");
        }
    }

    [Authorize(Roles = "User, Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<AdDto>> UpdateAd(int id, AdUpdateDto adUpdateDto)
    {
        try
        {
            var ad = await _adRepository.GetAdById(id);
            if (ad == null)
            {
                return NotFound();
            }

            ad.Address.ZipCode = adUpdateDto.Address.ZipCode;
            ad.Address.City = adUpdateDto.Address.City;
            ad.Address.Street = adUpdateDto.Address.Street;
            ad.Address.HouseNumber = adUpdateDto.Address.HouseNumber;
            ad.Rooms = adUpdateDto.Rooms;
            ad.Size = adUpdateDto.Size;
            ad.Price = adUpdateDto.Price;
            ad.Description = adUpdateDto.Description;

            _adRepository.UpdateAd(ad);
            return Ok(_classConverterService.AdToAdDto(ad));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error update ad data.");
            return BadRequest("Error update ad data.");
        }
    }

    [Authorize(Roles = "User, Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<AdDto>> DeleteAd(int id)
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

    [Authorize(Roles = "Admin")]
    [HttpPut("approve/{id}")]
    public async Task<IActionResult> ApproveAd(int id)
    {
        try
        {
            var ad = await _adRepository.GetAdById(id);
            if (ad == null)
            {
                return NotFound();
            }

            ad.Approved = true;

            _adRepository.UpdateAd(ad);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error update ad data.");
            return BadRequest("Error update ad data.");
        }
    }
}