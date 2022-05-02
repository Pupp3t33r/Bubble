namespace Bubble.CQS.Command;
public class AddNewArticlesCommand: IRequest<int>
{
    public IEnumerable<Article> ArticlesToWrite { get; set; }
}
