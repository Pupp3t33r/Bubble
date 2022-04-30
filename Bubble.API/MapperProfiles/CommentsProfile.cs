using AutoMapper;
using Bubble.Data.Entities;
using Bubble.Shared.Models.Response;

namespace Bubble.API.MapperProfiles;

public class CommentsProfile:Profile
{
    public CommentsProfile()
    {
        CreateMap<Comment, GetCommentsResponse>()
            .ForMember(resp=>resp.Username, 
            opt=>opt.MapFrom(com=>com.User.Name));
    }

}
