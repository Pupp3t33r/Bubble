namespace Bubble.Shared.Models.Request;
public class AddCommentRequest
{
    public string CommentText { get; set; }
    public Guid ArticleId { get; set; }
    public string UserName { get; set; }
}
