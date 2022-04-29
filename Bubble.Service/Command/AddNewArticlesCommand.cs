namespace Bubble.CQRS.Command;
public class AddNewArticlesCommand: IRequest<int>
{
    public IEnumerable<Article> ArticlesToWrite { get; set; }
}
