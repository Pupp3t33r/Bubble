using Bubble.Shared.Models.Request;

namespace Bubble.CQRS.Command;
public class AddCommentCommand: IRequest<int>
{
    public AddCommentRequest CommentRequest { get; set; }
}
