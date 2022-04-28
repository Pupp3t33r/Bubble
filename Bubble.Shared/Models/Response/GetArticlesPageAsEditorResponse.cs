namespace Bubble.Shared.Models.Response;
public class GetArticlesPageAsEditorResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ShortDesc { get; set; }
    public string Source { get; set; }
    public string SourceURL { get; set; }
    public DateTime PublishDate { get; set; }
    public bool Approved { get; set; }
    public int? GoodnessRating { get; set; }
}
