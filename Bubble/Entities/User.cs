using System.ComponentModel.DataAnnotations.Schema;

namespace Bubble.Data.Entities;
public class User : BaseEntity
{
    public string Name { get; set; }
    public string EncryptedPassword { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
    [ForeignKey(nameof(RoleId))] public AccessRole Role { get; set; }


    public IEnumerable<Comment> Comments { get; set; }
}
