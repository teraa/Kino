using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kino.Data;

[UsedImplicitly]
internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KinoDbContext>
{
    public KinoDbContext CreateDbContext(string[] args)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;

        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false)
           .AddJsonFile($"appsettings.{environment}.json", optional: false)
           .Build();

        var dbOptions = config.GetOptions<DbOptions>();

        var optionsBuilder = new DbContextOptionsBuilder<KinoDbContext>()
            .UseNpgsql(dbOptions.ConnectionString);

        return new KinoDbContext(optionsBuilder.Options);
    }
}
