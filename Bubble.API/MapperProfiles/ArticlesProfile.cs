﻿using AutoMapper;
using Bubble.Data.Entities;
using Bubble.Datascrapper.Types;
using Bubble.Shared.Models.Response;

namespace Bubble.API.MapperProfiles;

public class ArticlesProfile: Profile
{
    public ArticlesProfile()
    {
        CreateMap<ArticleRecord, Article>()
            .ForMember(art => art.PublishDate,
            opt => opt.MapFrom(artRec => artRec.pubDate.DateTime))
            .ForMember(art=>art.ArticleText, 
            opt=>opt.MapFrom(artRec=>artRec.text))
            .ForMember(art=>art.Title, 
            opt=>opt.MapFrom(artRec=>artRec.title))
            .ForMember(art=>art.SourceURL,
            opt=>opt.MapFrom(artRec=>artRec.link))
            .ForMember(art=>art.ShortDesc, 
            opt=> opt.MapFrom(artRec=>artRec.shortText));

        CreateMap<Article, GetArticlesPageAsReaderResponse>();
        CreateMap<Article, GetArticlesPageAsEditorResponse>();

        CreateMap<Tag, GetTagsResponse>();

        CreateMap<Comment, GetCommentsResponse>()
            .ForMember(commentResp => commentResp.Username,
            opt => opt.MapFrom(src => src.User.Name));
    }
}
