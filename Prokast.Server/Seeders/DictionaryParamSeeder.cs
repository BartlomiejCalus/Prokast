using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class DictionaryParamSeeder
    {
        public static void Seed(ProkastServerDbContext dbContext)
        {
            if(!dbContext.DictionaryParams.Any())
            {
                var dictionaryParamList = new List<DictionaryParams>()
                {
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Czerwony",
                        OptionID = 1,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Red",
                        OptionID = 1,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Rot",
                        OptionID = 1,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Červený",
                        OptionID = 1,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Niebieski",
                        OptionID = 2,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Blue",
                        OptionID = 2,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Blau",
                        OptionID = 2,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Modrý",
                        OptionID = 2,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Zielony",
                        OptionID = 3,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Green",
                        OptionID = 3,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Grün",
                        OptionID = 3,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Zelený",
                        OptionID = 3,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Żółty",
                        OptionID = 4,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Yellow",
                        OptionID = 4,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Gelb",
                        OptionID = 4,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Žluť",
                        OptionID = 4,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Brązowy",
                        OptionID = 5,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Brown",
                        OptionID = 5,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Braun",
                        OptionID = 5,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Hnědý",
                        OptionID = 5,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Fioletowy",
                        OptionID = 6,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Violet",
                        OptionID = 6,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Lila",
                        OptionID = 6,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Fialová",
                        OptionID = 6,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Biały",
                        OptionID = 7,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "White",
                        OptionID = 7,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Weiß",
                        OptionID = 7,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Bílý",
                        OptionID = 7,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Czarny",
                        OptionID = 8,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Black",
                        OptionID = 8,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Schwarz",
                        OptionID = 8,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Černý",
                        OptionID = 8,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Kolor",
                        Type = "String",
                        Value = "Różowy",
                        OptionID = 9,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Colour",
                        Type = "String",
                        Value = "Pink",
                        OptionID = 9,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Farbe",
                        Type = "String",
                        Value = "Rosa",
                        OptionID = 9,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Barva",
                        Type = "String",
                        Value = "Růžový",
                        OptionID = 9,
                        RegionID = 4,
                    },

                };
            }
        }
    }
}
