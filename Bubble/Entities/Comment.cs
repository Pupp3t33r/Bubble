using System.ComponentModel.DataAnnotations.Schema;

namespace Bubble.Data.Entities;
public class Comment : BaseEntity
{
    public string CommentText { get; set; }
    public DateTime PostTime { get; set; }
    public Guid ArticleId { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey(nameof(ArticleId))] public Article Article { get; set; }
    [ForeignKey(nameof(UserId))] public User User { get; set; }
}
