
using AutoMapper;
using Bubble.Data.Entities;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;

namespace Bubble.API.MapperProfiles;

public class UsersProfile: Profile
{
    public UsersProfile()
    {
        CreateMap<CreateUserRequest, User>().ReverseMap();
    }
}
