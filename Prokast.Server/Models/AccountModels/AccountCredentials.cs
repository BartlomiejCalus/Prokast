﻿using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.AccountModels
{
    public class AccountCredentials
    {
        [Required]
        public string Login {  get; set; }
        [Required]
        public string Password { get; set; }
    }
}
