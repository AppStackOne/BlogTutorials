using BenchmarkDotNet.Running;
using Benchmarks.FirstOrSingle;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var context = CreateDbContext();
await DataSeeder.SeedAsync(context);
context.MyEntities.First(x => x.ExternalId == Guid.Parse("E3DA4B2F-E3CD-45DE-9A20-C45C0B6AAA25"));
context.MyEntities.Single(x => x.ExternalId == Guid.Parse("E3DA4B2F-E3CD-45DE-9A20-C45C0B6AAA25"));

//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
BenchmarkRunner.Run<FirstOrSingleByGuidBenchmark>();

MyDbContext CreateDbContext()
{
    var dbContextOptions = new DbContextOptionsBuilder<MyDbContext>().UseSqlServer("Server=tcp:localhost,1433;User ID=sa;Password=YourStrong!a\u00b1;MultipleActiveResultSets=False;Encrypt=False;")
        .EnableSensitiveDataLogging()
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
        .Options;

    return new MyDbContext(dbContextOptions);
}