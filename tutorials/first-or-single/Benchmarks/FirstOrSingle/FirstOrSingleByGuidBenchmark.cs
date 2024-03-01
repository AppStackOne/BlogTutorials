using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Benchmarks.FirstOrSingle;

[MemoryDiagnoser]
public class FirstOrSingleByGuidBenchmark
{
    private MsSqlContainer _container;
    private DbContextOptions<MyDbContext> _dbContextOptions;
    private Guid _randomExternalId;
    private readonly List<Guid> _externalIds = DataSeeder.ExternalIds;

    [GlobalSetup]
    public void Setup()
    {
        _container = new MsSqlBuilder().Build();
        _container.StartAsync().GetAwaiter().GetResult();

        _dbContextOptions = new DbContextOptionsBuilder<MyDbContext>().UseSqlServer(_container.GetConnectionString())
            .Options;

        using var context = CreateContext();
        DataSeeder.SeedAsync(context).GetAwaiter().GetResult();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        var index = new Random().Next(_externalIds.Count);
        _randomExternalId = _externalIds[index];
    }

    [Benchmark]
    public async Task<MyEntity> SingleByGuid()
    {
        await using var context = CreateContext();
        return await context.MyEntities.SingleAsync(e => e.ExternalId == _randomExternalId);
    }

    [Benchmark(Baseline = true)]
    public async Task<MyEntity> FirstByGuid()
    {
        await using var context = CreateContext();
        return await context.MyEntities.FirstAsync(e => e.ExternalId == _randomExternalId);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _container.DisposeAsync().GetAwaiter().GetResult();
    }

    private MyDbContext CreateContext() => new(_dbContextOptions);
}