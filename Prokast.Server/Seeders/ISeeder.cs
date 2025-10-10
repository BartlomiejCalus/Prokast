using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public interface ISeeder
    {
        public int SeedOrder {  get; }
        void Seed(ProkastServerDbContext dbContext);
    }
}
