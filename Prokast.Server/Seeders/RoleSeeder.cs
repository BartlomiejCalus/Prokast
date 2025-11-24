using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class RoleSeeder : ISeeder
    {
        public int SeedOrder { get; init; } = 3;

        public void Seed(ProkastServerDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                var roleList = new List<Role>()
                {
                    new() {RoleName = "Master"},
                    new() {RoleName = "HeadAdmin"},
                    new() {RoleName = "Admin"},
                    new() {RoleName = "Magazynier"},
                    new() {RoleName = "Sprzedawca"},
                    new() {RoleName = "Default"}
                };
                dbContext.Roles.AddRange(roleList);
                dbContext.SaveChanges();
            }
        }
    }
}