using Bubble.Shared.Models.Request;

namespace Bubble.APIServices.Interfaces;
public interface IUserService
{
    Task<Guid> AddUserAsync(CreateUserRequest request);
    Task<Guid> GetRoleIdByRoleName(string role);
    Task<Guid> FindUserIdByNameAsync(string Name);
    Task<bool> VerifyPasswordAsync(Guid UserId, string Password);
    Task<string> GetRoleByUserIdAsync(Guid Id);
    string CreateJwtToken(string UserName, string Role);
}
