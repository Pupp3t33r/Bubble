using AutoMapper;
using Bubble.Data.Entities;
using Bubble.Service.Command;
using Bubble.Service.Interfaces;
using Bubble.Shared.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bubble.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController: ControllerBase
{
    public static User user = new User();

    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService=userService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(UserLoginRequest request)
    {
        var userId = await _userService.FindUserIdByNameAsync(request.UserName);

        if (userId == Guid.Empty) return BadRequest("User does not exist");

        var passwordCheck = await _userService.VerifyPasswordAsync(userId, request.Password);

        if (!passwordCheck) return BadRequest("Wrong Password");

        var userRole = await _userService.GetRoleByUserIdAsync(userId);

        var JwtToken = _userService.CreateJwtToken(request.UserName, userRole);

        return JwtToken;
    }

    [HttpPost("RegisterNewUser")]
    public async Task<ActionResult<Guid>> RegisterNewUser(CreateUserRequest request)
    {
        try
        {
            return await _userService.AddUserAsync(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
