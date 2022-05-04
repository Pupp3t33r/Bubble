namespace Bubble.CQS.Command;
public class AddBodyToArticlesCommand: IRequest<int>
{
    public List<Article> Articles { get; set; }
}
