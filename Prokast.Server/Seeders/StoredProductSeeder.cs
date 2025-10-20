using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class StoredProductSeeder: ISeeder
    {
        public int SeedOrder { get; init; } = 6;

        public void Seed(ProkastServerDbContext dbContext)
        {
            if (!dbContext.StoredProducts.Any())
            {
                var warehouse = dbContext.Warehouses.FirstOrDefault(x => x.ID == 1);
                var product = dbContext.Products.FirstOrDefault(x => x.ID == 1);
                var product2 = dbContext.Products.FirstOrDefault(x => x.ID == 2);
                var storedProductList = new List<StoredProduct>()
                {
                    new()
                    {
                        Quantity = 5,
                        MinQuantity = 2,
                        Warehouse = warehouse,
                        Product = product
                    },
                    new()
                    {
                        Quantity = 15,
                        MinQuantity = 2,
                        Warehouse = warehouse,
                        Product = product2
                    }
                };
                dbContext.StoredProducts.AddRange(storedProductList);
                dbContext.SaveChanges();
            }
        }
    }
}
