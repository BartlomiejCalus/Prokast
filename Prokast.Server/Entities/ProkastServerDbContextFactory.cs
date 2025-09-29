using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Prokast.Server.Entities
{
    public class ProkastServerDbContextFactory: IDesignTimeDbContextFactory<ProkastServerDbContext>
    {
        public ProkastServerDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ProkastServerDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ProkastServerDbContext(optionsBuilder.Options);
        }
    }
}
