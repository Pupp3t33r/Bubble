﻿namespace Bubble.CQRS.Command;
public class DeleteArticleCommand: IRequest<int>
{
    public Guid ArticleId { get; set; }
}
