namespace Bubble.Shared.Models.Response;

public class GetArticlesResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ArticleText { get; set; }
    public string Source { get; set; }
    public decimal GoodnessIndex { get; set; }
}
