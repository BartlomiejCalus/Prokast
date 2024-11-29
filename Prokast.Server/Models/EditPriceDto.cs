﻿using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class EditPriceDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Netto { get; set; }
        [Required]
        public decimal VAT { get; set; }
    }
}
