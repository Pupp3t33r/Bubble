namespace Bubble.CQS.Command;
public class DeleteArticleCommand: IRequest<int>
{
    public Guid ArticleId { get; set; }
}
