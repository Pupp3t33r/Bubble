using AutoMapper;
using Bubble.APIServices.Interfaces;
using Bubble.Data.Entities;
using Bubble.CQS.Command;
using Bubble.CQS.Query;
using Bubble.Shared.Models.Request;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bubble.APIServices.Services;

public class UserService: IUserService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    public UserService(IMediator mediator, IMapper mapper, IConfiguration config)
    {
        (_mediator, _mapper, _config) = (mediator, mapper, config);
    }
    public async Task<Guid> AddUserAsync(CreateUserRequest request)
    {
        var NewUser = _mapper.Map<User>(request);
        NewUser.EncryptedPassword = GetPasswordHash(request.Password, _config["AppSettings:PasswordSalt"]);
        NewUser.RoleId = await GetRoleIdByRoleName("Reader");

        var response = await _mediator.Send(new CreateUserCommand
        {
            User = NewUser
        });
        return response.Id;
    }

    public async Task<Guid> FindUserIdByNameAsync(string Name)
    {
        return await _mediator.Send(new CheckIfUserExistsQuery { UserName = Name });
    }

    public async Task<bool> VerifyPasswordAsync(Guid UserId, string Password)
    {
        var userPasswordHash = await _mediator.Send(new GetUserPasswordHashQuery { UserId = UserId });

        if (userPasswordHash == null) return false;

        var passwordHashToVerify = GetPasswordHash(Password, _config["AppSettings:PasswordSalt"]);

        return userPasswordHash == passwordHashToVerify;
    }
    public async Task<string> GetRoleByUserIdAsync(Guid Id)
    {
        return await _mediator.Send(new GetRoleByUserIdQuery { UserId = Id });
    }

    public async Task<Guid> GetRoleIdByRoleName(string role)
    {
        var response = await _mediator.Send(new GetRoleIdByRoleNameQuery
        {
            Name = role
        });

        return response;
    }

    private string GetPasswordHash(string password, string salt)
    {
        var sha1 = SHA1.Create();
        var sha1Data = sha1.ComputeHash(Encoding.UTF8.GetBytes($"{salt}_{password}"));
        var hashedPassword = Encoding.UTF8.GetString(sha1Data);
        return hashedPassword;
    }

    public string CreateJwtToken(string UserName, string Role)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserName),
                new Claim(ClaimTypes.Role, Role)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config["AppSettings:Token"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
