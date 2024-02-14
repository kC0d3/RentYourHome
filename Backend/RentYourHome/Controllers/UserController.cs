using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentYourHome.Models.Users;
using RentYourHome.Repositories.UserRepository;
using RentYourHome.Services.ClassConverterService;

namespace RentYourHome.Controllers;

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

    [Authorize(Roles = "User")]
    [HttpGet("{username}")]
    public async Task<ActionResult<UserDto>> GetUserByUserName(string username)
    {
        try
        {
            var user = await _userRepository.GetUserByUserName(username);
            return Ok(_classConverterService.UserToUserDto(user));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting user data.");
            return NotFound("Error getting user data.");
        }
    }

    [Authorize(Roles = "User, Admin")]
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

            user.Username = userReqDto.Username;
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

    [Authorize(Roles = "User, Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> DeleteUser(int id)
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