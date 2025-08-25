using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.PhotoModels
{
    public class PhotoAdd
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public byte[] Value { get; set; }
        [Required]
        public string ContentType { get; set; }
    }
}