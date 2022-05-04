using AutoMapper;
using Bubble.Data.Entities;
using Bubble.Datascrapper.Types;
using Bubble.Shared.Models.Response;

namespace Bubble.API.MapperProfiles;

public class ArticlesProfile: Profile
{
    public ArticlesProfile()
    {
        CreateMap<OnlinerArticleRecord, Article>()
            .ForMember(art => art.PublishDate,
            opt => opt.MapFrom(artRec => artRec.pubDate.DateTime))
            .ForMember(art=>art.ArticleText, 
            opt=>opt.MapFrom(artRec=>artRec.text))
            .ForMember(art=>art.Title, 
            opt=>opt.MapFrom(artRec=>artRec.title))
            .ForMember(art=>art.Source,
            opt=>opt.MapFrom(artRec=>artRec.source))
            .ForMember(art=>art.SourceURL,
            opt=>opt.MapFrom(artRec=>artRec.link))
            .ForMember(art=>art.ShortDesc, 
            opt=> opt.MapFrom(artRec=>artRec.shortText));

        CreateMap<LentaArticleRecord, Article>()
            .ForMember(art => art.PublishDate,
            opt => opt.MapFrom(artRec => artRec.pubDate.DateTime))
            .ForMember(art => art.ArticleText,
            opt => opt.MapFrom(artRec => artRec.text))
            .ForMember(art => art.Title,
            opt => opt.MapFrom(artRec => artRec.title))
            .ForMember(art => art.Source,
            opt => opt.MapFrom(artRec => artRec.source))
            .ForMember(art => art.SourceURL,
            opt => opt.MapFrom(artRec => artRec.link))
            .ForMember(art => art.ShortDesc,
            opt => opt.MapFrom(artRec => artRec.shortText));

        CreateMap<Article, GetArticlesPageAsReaderResponse>();
        CreateMap<Article, GetArticlesPageAsEditorResponse>();
        CreateMap<Article, GetArticleResponse>();

        CreateMap<Tag, GetTagsResponse>();

        CreateMap<Comment, GetCommentsResponse>()
            .ForMember(commentResp => commentResp.Username,
            opt => opt.MapFrom(src => src.User.Name));
    }
}
