
namespace Bubble.Data.Entities;
public class AccessRole : BaseEntity
{
    public string Name { get; set; }

    public List<User> Users { get; set; }
}
