namespace Bubble.Shared.Models.Response;
public class GetArticlesAsReaderResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ArticleText { get; set; }
    public string Source { get; set; }
    public string SourceURL { get; set; }
    public DateTime PublishDate { get; set; }
}
