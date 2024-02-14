using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentYourHome.Models.Users;
using RentYourHome.Repositories.UserRepository;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IClassConverterService _classConverterService;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository,
        IClassConverterService classConverterService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _classConverterService = classConverterService;
    }

    [HttpPost]
    public ActionResult<UserReqDto> CreateUser([Required] UserReqDto user)
    {
        try
        {
            _userRepository.AddUserToDb(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error create user.");
            return BadRequest("Error create user.");
        }
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<UserDto>> GetUserByUserName(string userName)
    {
        try
        {
            var user = await _userRepository.GetUserByUserName(userName);
            return Ok(_classConverterService.UserToUserDto(user));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting user data.");
            return NotFound("Error getting user data.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UserReqDto userReqDto)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = userReqDto.UserName;
            user.FirstName = userReqDto.FirstName;
            user.LastName = userReqDto.LastName;
            user.Email = userReqDto.Email;

            _userRepository.UpdateUser(user);
            return Ok(_classConverterService.UserToUserDto(user));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error update user data.");
            return BadRequest("Error update user data.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> DeleteUserById(int id)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            _userRepository.DeleteUser(user);
            return Ok(_classConverterService.UserToUserDto(user));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting user data.");
            return NotFound("Error getting user data.");
        }
    }
}