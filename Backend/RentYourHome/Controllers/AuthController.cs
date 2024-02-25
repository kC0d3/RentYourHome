using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RentYourHome.Contracts;
using RentYourHome.Models.Users;
using RentYourHome.Repositories.UserRepository;
using RentYourHome.Services.Authentication;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IClassConverterService _classConverterService;

    public AuthController(IAuthService authenticationService,IUserRepository userRepository,
        IClassConverterService classConverterService)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _classConverterService = classConverterService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result =
            await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, "User");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }
        
        try
        {
            _userRepository.AddUserToDb(_classConverterService.RegistrationRequestToToUser(request));
            return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.Username));
        }
        catch (Exception e)
        {
            return BadRequest("Error create user.");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.Username, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }
        
        Response.Cookies.Append("token", result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
        return Ok(result.Role);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("User logged out successfully.");
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
}