namespace Bubble.CQRS.Command;
public class ChangeArticleApprovalCommand: IRequest<bool>
{
    public Guid ArticleId { get; set; }
}
