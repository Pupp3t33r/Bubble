﻿namespace Bubble.CQS.Query;
public class GetArticleQuery : IRequest<Article>
{
    public Guid ArticleId { get; set; }
}
