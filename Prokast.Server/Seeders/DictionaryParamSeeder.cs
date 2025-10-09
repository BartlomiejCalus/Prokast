using Prokast.Server.Entities;

namespace Prokast.Server.Seeders
{
    public class DictionaryParamSeeder: ISeeder
    {
        public void Seed(ProkastServerDbContext dbContext)
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
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "100ml",
                        OptionID = 1,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "100ml",
                        OptionID = 1,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "100ml",
                        OptionID = 1,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "100ml",
                        OptionID = 1,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "200ml",
                        OptionID = 2,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "200ml",
                        OptionID = 2,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "200ml",
                        OptionID = 2,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "200ml",
                        OptionID = 2,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "250ml",
                        OptionID = 3,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "250ml",
                        OptionID = 3,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "250ml",
                        OptionID = 3,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "250ml",
                        OptionID = 3,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "300ml",
                        OptionID = 4,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "300ml",
                        OptionID = 4,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "300ml",
                        OptionID = 4,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "300ml",
                        OptionID = 4,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "400ml",
                        OptionID = 5,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "400ml",
                        OptionID = 5,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "400ml",
                        OptionID = 5,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "400ml",
                        OptionID = 5,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "500ml",
                        OptionID = 6,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "500ml",
                        OptionID = 6,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "500ml",
                        OptionID = 6,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "500ml",
                        OptionID = 6,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "750ml",
                        OptionID = 7,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "750ml",
                        OptionID = 7,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "750ml",
                        OptionID = 7,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "750ml",
                        OptionID = 7,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "1l",
                        OptionID = 8,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "1l",
                        OptionID = 8,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "1l",
                        OptionID = 8,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "1l",
                        OptionID = 8,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "1,5l",
                        OptionID = 9,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "1,5l",
                        OptionID = 9,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "1,5l",
                        OptionID = 9,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "1,5l",
                        OptionID = 9,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "2l",
                        OptionID = 10,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "2l",
                        OptionID = 10,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "2l",
                        OptionID = 10,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "2l",
                        OptionID = 10,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "2,5l",
                        OptionID = 11,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "2,5l",
                        OptionID = 11,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "2,5l",
                        OptionID = 11,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "2,5l",
                        OptionID = 11,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "5l",
                        OptionID = 12,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "5l",
                        OptionID = 12,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "5l",
                        OptionID = 12,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "5l",
                        OptionID = 12,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Pojemność",
                        Type = "String",
                        Value = "10l",
                        OptionID = 13,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Volume",
                        Type = "String",
                        Value = "10l",
                        OptionID = 13,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Volumen",
                        Type = "String",
                        Value = "10l",
                        OptionID = 13,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Objem",
                        Type = "String",
                        Value = "10l",
                        OptionID = 13,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Sezon",
                        Type = "String",
                        Value = "Wiosna",
                        OptionID = 1,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Season",
                        Type = "String",
                        Value = "Spring",
                        OptionID = 1,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Jahreszeit",
                        Type = "String",
                        Value = "Frühling",
                        OptionID = 1,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Sezóna",
                        Type = "String",
                        Value = "Jaro",
                        OptionID = 1,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Sezon",
                        Type = "String",
                        Value = "Lato",
                        OptionID = 2,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Season",
                        Type = "String",
                        Value = "Summer",
                        OptionID = 2,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Jahreszeit",
                        Type = "String",
                        Value = "Sommer",
                        OptionID = 1,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Sezóna",
                        Type = "String",
                        Value = "Letní",
                        OptionID = 2,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Sezon",
                        Type = "String",
                        Value = "Jesień",
                        OptionID = 3,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Season",
                        Type = "String",
                        Value = "Autumn",
                        OptionID = 2,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Jahreszeit",
                        Type = "String",
                        Value = "Herbst",
                        OptionID = 1,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Sezóna",
                        Type = "String",
                        Value = "Podzim",
                        OptionID = 2,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Sezon",
                        Type = "String",
                        Value = "Zima",
                        OptionID = 4,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Season",
                        Type = "String",
                        Value = "Winter",
                        OptionID = 4,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Jahreszeit",
                        Type = "String",
                        Value = "Winter",
                        OptionID = 4,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Sezóna",
                        Type = "String",
                        Value = "Zima",
                        OptionID = 4,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Grupa docelowa",
                        Type = "String",
                        Value = "Dzieci",
                        OptionID = 1,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Target group",
                        Type = "String",
                        Value = "Children",
                        OptionID = 1,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Zielgruppe",
                        Type = "String",
                        Value = "Kinder",
                        OptionID = 1,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Cílová skupina",
                        Type = "String",
                        Value = "Děti",
                        OptionID = 1,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Grupa docelowa",
                        Type = "String",
                        Value = "Młodzież",
                        OptionID = 2,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Target group",
                        Type = "String",
                        Value = "Youth",
                        OptionID = 2,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Zielgruppe",
                        Type = "String",
                        Value = "Jugend",
                        OptionID = 2,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Cílová skupina",
                        Type = "String",
                        Value = "Mládí",
                        OptionID = 1,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Grupa docelowa",
                        Type = "String",
                        Value = "Dorośli",
                        OptionID = 3,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Target group",
                        Type = "String",
                        Value = "Adults",
                        OptionID = 3,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Zielgruppe",
                        Type = "String",
                        Value = "Erwachsene",
                        OptionID = 3,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Cílová skupina",
                        Type = "String",
                        Value = "Dospělí",
                        OptionID = 3,
                        RegionID = 4,
                    },
                    new()
                    {
                        Name = "Grupa docelowa",
                        Type = "String",
                        Value = "Seniorzy",
                        OptionID = 4,
                        RegionID = 1,
                    },
                    new()
                    {
                        Name = "Target group",
                        Type = "String",
                        Value = "Seniors",
                        OptionID = 4,
                        RegionID = 2,
                    },
                    new()
                    {
                        Name = "Zielgruppe",
                        Type = "String",
                        Value = "Senioren",
                        OptionID = 4,
                        RegionID = 3,
                    },
                    new()
                    {
                        Name = "Cílová skupina",
                        Type = "String",
                        Value = "Senioři",
                        OptionID = 4,
                        RegionID = 4,
                    },
                };
                //dbContext.DictionaryParams.AddRange(dictionaryParamList);
                //dbContext.SaveChanges();
            }
        }
    }
}
