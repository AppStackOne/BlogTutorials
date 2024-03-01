namespace Benchmarks.FirstOrSingle;

public static class DataSeeder
{
    public static List<Guid> ExternalIds { get; } = Enumerable.Range(0, 1000000).Select(_ => Guid.NewGuid()).ToList();
    
    public static async Task SeedAsync(MyDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (context.MyEntities.Any())
        {
            return;
        }

        var entities = ExternalIds
            .Select(externalId => new MyEntity { ExternalId = externalId })
            .ToList();

        await context.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }
}