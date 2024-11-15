﻿using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class LoginRequest
    {
        [Required]
        public string Login {  get; set; }
        [Required]
        public string Password { get; set; }
    }
}
