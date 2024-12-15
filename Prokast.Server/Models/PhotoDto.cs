﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class PhotoDto 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
