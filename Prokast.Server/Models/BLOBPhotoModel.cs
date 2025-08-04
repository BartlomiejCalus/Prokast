using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class BLOBPhotoModel 
    {
        [Required] 
        public string Name { get; set; }

        [Required]
        public byte[] Value { get; set; }

        [Required]
        public string ContentType { get; set; }
    }
}
