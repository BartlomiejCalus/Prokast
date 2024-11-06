﻿using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}