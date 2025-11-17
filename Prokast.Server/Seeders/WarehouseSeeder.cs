using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class WarehouseSeeder: ISeeder
    {
        public int SeedOrder { get; init; } = 6;

        public void Seed(ProkastServerDbContext dbContext)
        {
            if (!dbContext.Warehouses.Any())
            {
                var region = dbContext.Regions.FirstOrDefault(x => x.Name == "PL");
                var client = dbContext.Clients.FirstOrDefault(x => x.ID == 1);
                var client2 = dbContext.Clients.FirstOrDefault(x => x.ID == 2);

                var warehouseList = new List<Warehouse>()
                {
                    new()
                    {
                        Name = "Wielki Magazyn Alibaby",
                        Address = "Krupnicza 12",
                        PostalCode = "12-345",
                        City = "Poznań",
                        Country = "Poland",
                        PhoneNumber = "123-321-342",
                        Client = client,
                        Accounts = new List<Account>
                        {
                            dbContext.Accounts.FirstOrDefault(x => x.Login.Contains("metalowiec")),
                            dbContext.Accounts.FirstOrDefault(x => x.Login.Contains("marmar"))
                        }
                    },
                    new()
                    {
                        Name = "Kosmiczne Emporium Pana Marudy",
                        Address = "Zabawna 32",
                        PostalCode = "13-335",
                        City = "Gdańsk",
                        Country = "Poland",
                        PhoneNumber = "321-653-812",
                        Client = client2,
                        Accounts = new List<Account>
                        {
                            dbContext.Accounts.FirstOrDefault(x => x.Login.Contains("korniszon")),
                            dbContext.Accounts.FirstOrDefault(x => x.Login.Contains("444"))
                        }
                    }
                };
                dbContext.Warehouses.AddRange(warehouseList);
                dbContext.SaveChanges();
            }
        }
    }
}
