﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Photo 
    {
        [Key]
        public int Id { get; set; }
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
