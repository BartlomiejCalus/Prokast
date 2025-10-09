using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class RegionSeeder
    {
        public static void Seed(ProkastServerDbContext dbContext)
        {
            if (!dbContext.Regions.Any())
            {
                var regionList = new List<Region>()
                {
                    new() {Name = "PL"},
                    new() {Name = "EN"},
                    new() {Name = "DE"},
                    new() {Name = "CZ"}
                };
                dbContext.Regions.AddRange(regionList);
                dbContext.SaveChanges();
            }
        }
    }
}
