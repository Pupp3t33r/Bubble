using System.ComponentModel.DataAnnotations;

namespace Bubble.Data.Entities;

public class BaseEntity
{
    [Key] public Guid Id { get; set; }
}
