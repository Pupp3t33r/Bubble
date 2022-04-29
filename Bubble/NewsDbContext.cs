using Bubble.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bubble.Data;
public class NewsDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AccessRole> AccessRoles { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasMany(x => x.Tags).WithMany(x => x.Articles)
                                        .UsingEntity(j => j.ToTable("articletags"));
    }

}
