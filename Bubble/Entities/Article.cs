using System.ComponentModel.DataAnnotations.Schema;

namespace Bubble.Data.Entities;
public class Article : BaseEntity
{
    public string Title { get; set; }
    public string ArticleText { get; set; }
    public string Source { get; set; }
    public DateTime PublishDate { get; set; }
    [Column(TypeName = "decimal(3, 2)")] public decimal GoodnessIndex { get; set; }

    public IEnumerable<Tag> Tags { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}
