namespace Bubble.CQS.Command;
public class ChangeArticleApprovalCommand: IRequest<bool>
{
    public Guid ArticleId { get; set; }
}
