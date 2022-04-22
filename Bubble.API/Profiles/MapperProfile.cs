
using AutoMapper;
using Bubble.Data.Entities;
using Bubble.Shared.Models.Request;
using Bubble.Shared.Models.Response;

namespace Bubble.API.Profiles;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Tag, GetTagsResponse>();

        CreateMap<Comment, GetCommentsResponse>()
            .ForMember(commentResp=>commentResp.Username,
            opt=>opt.MapFrom(src=>src.User.Name));

        CreateMap<Article, GetArticlesResponse>();

        CreateMap<CreateUserRequest, User>().ReverseMap();
    }
}
