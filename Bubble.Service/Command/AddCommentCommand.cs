using Bubble.Shared.Models.Request;

namespace Bubble.CQS.Command;
public class AddCommentCommand: IRequest<int>
{
    public AddCommentRequest CommentRequest { get; set; }
}
