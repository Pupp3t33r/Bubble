using System.ComponentModel.DataAnnotations;

namespace Bubble.Data.Entities;
public class Tag : BaseEntity
{
    [Required] public string Name { get; set; }

    public IEnumerable<Article> Articles { get; set; }
}
