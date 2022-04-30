namespace Bubble.Shared.Models.Response;
public class GetCommentsResponse
{
    public Guid Id { get; set; }
    public string CommentText { get; set; }
    public string Username { get; set; }
    public DateTime PostTime { get; set; }
}
