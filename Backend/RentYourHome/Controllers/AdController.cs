using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RentYourHome.Models.Ads;
using RentYourHome.Repositories.AdRepository;

namespace RentYourHome.Controllers;

[ApiController]
[Route("api/ads")]
public class AdController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IAdRepository _adRepository;

    public AdController(ILogger<UserController> logger, IAdRepository adRepository)
    {
        _logger = logger;
        _adRepository = adRepository;
    }
    [HttpPost("create")]
    public ActionResult<AdDto> PostAd([Required] AdDto ad)
    {
        try
        {
            _adRepository.AddAdToDb(ad);
            return Ok();
        }
        catch (Exception e)
        {
            //_logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }

    [HttpGet("all")]
    public ActionResult<int> GetAds()
    {
        try
        {
            return Ok(1);
        }
        catch (Exception e)
        {
            //_logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }

    [HttpGet("ad/{id}")]
    public ActionResult<int> GetAdById(int id)
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            //_logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }

    [HttpDelete("ad/{id}")]
    public ActionResult<int> DeleteAdById(int id)
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            //_logger.LogError(e, "Error getting ad data.");
            return NotFound("Error getting ad data.");
        }
    }
}