using Prokast.Server.Entities;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using System.Text;
using System.Security.Cryptography;


namespace Prokast.Server.Seeders
{
    public class ClientSeeder: ISeeder
    {
        public int SeedOrder { get; init; } = 3;

        public static string getHashed(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
        public void Seed(ProkastServerDbContext dbContext)
        {
            if (!dbContext.Clients.Any())
            {
                var clientList = new List<Client>()
                {
                    new()
                    {
                        FirstName = "Mariusz",
                        LastName = "Metalowiec",
                        BusinessName = "Metale Nieszlachetne Spółka Bezakcyjna",
                        NIP = "PL-123-123123-1",
                        Address = "Anglojęzyczna 12",
                        PhoneNumber = "123456789",
                        PostalCode = "11-111",
                        City = "Gliwice",
                        Country = "Poland",
                        
                        Accounts = new List<Account>()
                        {
                            new()
                            {
                                Login = "mariuszmetalowiec",
                                Password = getHashed("password"),
                            },
                            new()
                            {
                                Login = "marmar123",
                                Password = getHashed("marmar"),
                            }
                        }
                    },
                    new()
                    {
                        FirstName = "Albert",
                        LastName = "Korniszon",
                        BusinessName = "Ogórki Zgniłe I Nawet Spleśniałe",
                        NIP = "PL-321-333123-1",
                        Address = "Onomatopejska 10",
                        PhoneNumber = "165456788",
                        PostalCode = "12-213",
                        City = "Inowrocław",
                        Country = "Poland",

                        Accounts = new List<Account>()
                        {
                            new()
                            {
                                Login = "albertkorniszon",
                                Password = getHashed("albert"),
                            },
                            new()
                            {
                                Login = "ugauga444",
                                Password = getHashed("ugaaa"),
                            }
                        }
                    }
                };
                dbContext.Clients.AddRange(clientList);
                dbContext.SaveChanges();
            }
        }
    }
}
