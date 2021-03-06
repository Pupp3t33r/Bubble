using System.ComponentModel.DataAnnotations.Schema;

namespace Bubble.Data.Entities;
public class Article : BaseEntity
{
    public string Title { get; set; }
    public string ShortDesc { get; set; }
    public string? ArticleText { get; set; }
    public string Source { get; set; }
    public string SourceURL { get; set; }
    public DateTime PublishDate { get; set; }
    public int? GoodnessRating { get; set; }
    public bool Approved { get; set; } = false;

    public IEnumerable<Tag> Tags { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}
