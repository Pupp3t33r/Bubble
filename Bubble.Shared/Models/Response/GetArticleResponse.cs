namespace Bubble.Shared.Models.Response;
public class GetArticleResponse
{
    public string Title { get; set; }
    public string ArticleText { get; set; }
    public string Source { get; set; }
    public string SourceURL { get; set; }
    public DateTime PublishDate { get; set; }
}
