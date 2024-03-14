using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentYourHome.Models.UserAdApplications;
using RentYourHome.Repositories.AdRepository;
using RentYourHome.Repositories.UserAdApplicationRepository;

namespace RentYourHome.Controllers;

[ApiController]
[Route("api/useradapplication")]
public class UserAdApplicationController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IAdRepository _adRepository;
    private readonly IUserAdApplicationRepository _userAdApplicationRepository;

    public UserAdApplicationController(ILogger<UserController> logger, IAdRepository adRepository,
        IUserAdApplicationRepository userAdApplicationRepository)
    {
        _logger = logger;
        _adRepository = adRepository;
        _userAdApplicationRepository = userAdApplicationRepository;
    }

    [Authorize(Roles = "User")]
    [HttpPost("apply/{adId}&{userId}")]
    public async Task<IActionResult> ApplyAd(int adId, int userId)
    {
        try
        {
            var ad = await _adRepository.GetAdById(adId);
            if (ad == null)
            {
                return NotFound();
            }

            ad.UserAdApplications.Add(new UserAdApplication { AdId = adId, UserId = userId });

            _adRepository.UpdateAd(ad);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error update ad data.");
            return BadRequest("Error update ad data.");
        }
    }

    [Authorize(Roles = "User")]
    [HttpDelete("cancel/{adId}&{userId}")]
    public async Task<IActionResult> CancelApplyAd(int adId, int userId)
    {
        try
        {
            var userAdApplication = await _userAdApplicationRepository.GetUserAdApplication(adId, userId);
            if (userAdApplication == null)
            {
                return NotFound();
            }

            _userAdApplicationRepository.DeleteUserAdApplication(userAdApplication);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error update ad data.");
            return BadRequest("Error update ad data.");
        }
    }
}