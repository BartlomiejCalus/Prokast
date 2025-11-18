using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class ProductSeeder: ISeeder
    {
        public int SeedOrder { get; init; } = 5;
        public void Seed(ProkastServerDbContext dbContext)
        {
            if (!dbContext.Products.Any())
            {
                var region = dbContext.Regions.FirstOrDefault(x => x.Name == "PL");
                var client = dbContext.Clients.FirstOrDefault(x => x.ID == 1);
                var productList = new List<Product>()
                {
                    new()
                    {
                        Name = "Imbryczek.",
                        SKU = "123GHY4FF7",
                        EAN = "LKJH0009GV",
                        Description = "Imbryczek jest ładny.",
                        AdditionalDescriptions = new List<AdditionalDescription>()
                        {
                            new()
                            {
                                Title = "Opis Imbryczka.",
                                Value = "Imbryczek jest bardzo ładny.",
                                Regions = region
                            },
                            new()
                            {
                                Title = "Opis Imbryczka.",
                                Value = "Imbryczek się ładnie świeci, żonie się podoba.",
                                Regions = region
                            }
                        },
                        AdditionalNames = new List<AdditionalName>()
                        {
                            new()
                            {
                                Title = "Nazwa Imbryczka.",
                                Value = "Piękny Imbryczek.",
                                Regions = region
                            },
                            new()
                            {
                                Title = "Nazwa Imbryczka.",
                                Value = "Zabytkowy Imbryczek Mojej Żony.",
                                Regions = region
                            }
                        },
                        CustomParams = new List<CustomParams>()
                        {
                            new()
                            {
                                Name = "Uchwyt",
                                Type = "String",
                                Value = "Ładny, ręcznie zdobiony uchwyt.",
                                Regions = region
                            },
                            new()
                            {
                                Name = "Podstawka",
                                Type = "String",
                                Value = "Ręcznie zdobiona podstawka, nawet czysta.",
                                Regions = region
                            }
                        },
                        DictionaryParams = new List<DictionaryParams>()
                        {
                            dbContext.DictionaryParams.FirstOrDefault(x => x.Value == "Biały"),
                            dbContext.DictionaryParams.FirstOrDefault(x => x.Value == "500ml" && x.Name == "Pojemność")
                        },
                        Photos = new List<Photo>()
                        {
                            new()
                            {
                                Name = "aaa_CID1_PID1.png",
                                Value = "https://prokast.blob.core.windows.net/images/aaa_CID1_PID1.png"
                            },
                            new()
                            {
                                Name = "asasasasaaa_CID1_PID1.png",
                                Value = "https://prokast.blob.core.windows.net/images/asasasasaaa_CID1_PID1.png"
                            }
                        },
                        PriceList = new()
                        {
                            Name = "Cennik Imbryczka",
                            Prices = new List<Prices>
                            {
                                new()
                                {
                                    Name = "Cena Imbryczka",
                                    Netto = 12.50m,
                                    VAT = 23,
                                    Brutto = 12.50m * 1.23m,
                                    Regions = region
                                }
                            }
                        },
                        Client = client
                    },
                    new()
                    {
                        Name = "Kociołek.",
                        SKU = "321GHYBVFF6",
                        EAN = "LKJH66BHG56",
                        Description = "Kociołek jest bardzo ładny i błyszczący.",
                        AdditionalDescriptions = new List<AdditionalDescription>()
                        {
                            new()
                            {
                                Title = "Opis Kociołka.",
                                Value = "Kociołek jest bardzo oryginalny i pięknie zrobiony.",
                                Regions = region
                            },
                            new()
                            {
                                Title = "Opis Kociołka.",
                                Value = "Kociołek się ładnie świeci, żonie się podoba.",
                                Regions = region
                            }
                        },
                        AdditionalNames = new List<AdditionalName>()
                        {
                            new()
                            {
                                Title = "Nazwa Kociołka.",
                                Value = "Piękny Kociołek.",
                                Regions = region
                            },
                            new()
                            {
                                Title = "Nazwa Kociołka.",
                                Value = "Zabytkowy Kociołek Mojej Babci.",
                                Regions = region
                            }
                        },
                        CustomParams = new List<CustomParams>()
                        {
                            new()
                            {
                                Name = "Uchwyty",
                                Type = "String",
                                Value = "Są 2, pięknie wykończone i jeszcze nie odpadły.",
                                Regions = region
                            },
                            new()
                            {
                                Name = "Pokrywka",
                                Type = "String",
                                Value = "Ręcznie zdobiona pokrywka, nawet czysta.",
                                Regions = region
                            }
                        },
                        DictionaryParams = new List<DictionaryParams>()
                        {
                            dbContext.DictionaryParams.FirstOrDefault(x => x.Value == "Czarny"),
                            dbContext.DictionaryParams.FirstOrDefault(x => x.Value == "2l" && x.Name == "Pojemność")
                        },
                        Photos = new List<Photo>()
                        {
                            new()
                            {
                                Name = "asas_CID1_PID1.png",
                                Value = "https://prokast.blob.core.windows.net/images/asas_CID1_PID1.png"
                            },
                            new()
                            {
                                Name = "asasasas_CID1_PID1.png",
                                Value = "https://prokast.blob.core.windows.net/images/asasasas_CID1_PID1.png"
                            }
                        },
                        PriceList = new()
                        {
                            Name = "Cennik Kociołka",
                            Prices = new List<Prices>
                            {
                                new()
                                {
                                    Name = "Cena Kociołka",
                                    Netto = 22.45m,
                                    VAT = 23,
                                    Brutto = 22.45m * 1.23m,
                                    Regions = region
                                }
                            }
                        },
                        Client = client
                    }
                };
                dbContext.Products.AddRange(productList);
                dbContext.SaveChanges();
            }
        }
    }
}
