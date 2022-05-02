namespace Bubble.CQS.Command;
public class RateArticleGoodnessCommand: IRequest<int>
{
    public Guid Id { get; set; }
    public int GoodnessRating { get; set; }
}
