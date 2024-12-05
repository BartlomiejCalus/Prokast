﻿using System.ComponentModel.DataAnnotations;
using Prokast.Server.Entities;

namespace Prokast.Server.Models
{
    public class ProductGet
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string EAN { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<AdditionalName>? AdditionalNames { get; set; }
        public List<DictionaryParams>? DictionaryParams { get; set; }
        public List<CustomParams>? CustomParams { get; set; }
        [Required]
        public PriceListAll PriceList { get; set; }
        public DateTime AdditionDate { get; set; } = DateTime.Now;
        public DateTime ModificationDate { get; set; } = DateTime.Now;
    }
}
