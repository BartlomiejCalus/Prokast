using Prokast.Server.Entities;
using System.Data;

namespace Prokast.Server.Seeders
{
    public class MainSeeder
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IEnumerable<ISeeder> _seeders;

        public MainSeeder(ProkastServerDbContext dbContext, IEnumerable<ISeeder> seeders)
        {
            _dbContext = dbContext;
            _seeders = seeders;
        }

        public void SeedDB()
        {
            var orderedSeeders = _seeders.OrderBy(x => x.SeedOrder);
            foreach (var seeder in orderedSeeders)
            {
                seeder.Seed(_dbContext);
            }
        }
    }
}

