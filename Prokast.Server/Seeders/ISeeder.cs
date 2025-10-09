using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public interface ISeeder
    {
        void Seed(ProkastServerDbContext dbContext);
    }
}
