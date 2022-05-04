namespace Bubble.CQS.Command;
public class WriteArticleTextsCommand : IRequest<int>
{
    public List<Article> Articles { get; set; }
}
