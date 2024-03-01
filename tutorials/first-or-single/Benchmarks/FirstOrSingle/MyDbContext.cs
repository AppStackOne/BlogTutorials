using Microsoft.EntityFrameworkCore;

namespace Benchmarks.FirstOrSingle;

public class MyDbContext : DbContext
{
    public DbSet<MyEntity> MyEntities { get; set; }

    public MyDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MyEntity>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered();
            entity.HasIndex(e => e.ExternalId).IsUnique();
        });
    }
}