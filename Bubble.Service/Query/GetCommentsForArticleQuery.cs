namespace Bubble.CQS.Query;
public class GetCommentsForArticleQuery : IRequest<List<Comment>>
{
    public Guid ArticleId { get; set; }
}
