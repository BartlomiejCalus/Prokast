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
            foreach (var seeder in _seeders)
            {
                seeder.Seed(_dbContext);
            }
        }
    }
}

