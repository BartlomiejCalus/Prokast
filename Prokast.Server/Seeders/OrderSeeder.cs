using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class OrderSeeder: ISeeder
    {
        public int SeedOrder { get; init; } = 7;

        public void Seed(ProkastServerDbContext dbContext)
        {
            if (!dbContext.Orders.Any())
            {
                var client = dbContext.Clients.FirstOrDefault(x => x.ID == 1);
                var product = dbContext.Products.FirstOrDefault(x => x.ID == 1);
                var product2 = dbContext.Products.FirstOrDefault(x => x.ID == 2);
                var orderList = new List<Order>()
                {
                    new()
                    {
                        OrderID = "Svh67nPO01S",
                        TotalPrice = 49.99m,
                        TotalWeightKg = 5.00m,
                        PaymentMethod = "Card",
                        IsBusiness = false,
                        Client = client,
                        Buyer = new()
                        {
                            FirstName = "Andrzej",
                            LastName = "Mistrz",
                            Email = "andrewmaster@wp.pl",
                            PhoneNumber = "543-870-647",
                            Address = "Mistrzowska 12",
                            HouseNumber = "12",
                            PostalCode = "54-823",
                            City = "Gdynia",
                            Country = "Poland"
                        },
                        OrderProducts = new List<OrderProduct>
                        {
                            new()
                            {
                                Product = product2
                            }
                        }
                    },
                    new()
                    {
                        OrderID = "JNH755hBgjn0L",
                        TotalPrice = 69.99m,
                        TotalWeightKg = 3.00m,
                        PaymentMethod = "Card",
                        IsBusiness = false,
                        Client = client,
                        Buyer = new()
                        {
                            FirstName = "Miłosz",
                            LastName = "Magdziarz",
                            Email = "miloszmagdziarz@wp.pl",
                            PhoneNumber = "233-970-318",
                            Address = "Mariacka 10",
                            HouseNumber = "15",
                            PostalCode = "23-117",
                            City = "Wrocław",
                            Country = "Poland"
                        },
                        OrderProducts = new List<OrderProduct>
                        {
                            new()
                            {
                                Product = product
                            }
                        }
                    }

                };
                dbContext.Orders.AddRange(orderList);
                dbContext.SaveChanges();
            }
        }
    }
}
