using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Prokast.Server.Entities;
using Prokast.Server.Seeders;
using Testcontainers.MsSql;

namespace Prokast.Tests.Infrastructure;
public class TestAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            var testSettings = new Dictionary<string, string?>
            {
                ["AppSettings:Issuer"] = "https://localhost",
                ["AppSettings:Audience"] = "https://localhost",
                ["AppSettings:Key"] = "testowy-super-sekret-ktory-na-pewno-dziala"
            };

            config.AddInMemoryCollection(testSettings);
        });

        builder.ConfigureServices(services =>
        {
            var dbDescriptor = services.SingleOrDefault(
                s => s.ServiceType == typeof(DbContextOptions<ProkastServerDbContext>));
            if (dbDescriptor != null)
                services.Remove(dbDescriptor);

            var connectionString = _dbContainer.GetConnectionString();

            services.AddDbContext<ProkastServerDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddMassTransitTestHarness();

            services.Configure<HealthCheckServiceOptions>(options => options.Registrations.Clear());

            services.AddHealthChecks()
                .AddDbContextCheck<ProkastServerDbContext>()
                .AddCheck("self", () => HealthCheckResult.Healthy());
        });
    }

    public async Task InitializeAsync() => await _dbContainer.StartAsync();

    public new async Task DisposeAsync() => await _dbContainer.StopAsync();
}