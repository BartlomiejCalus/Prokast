using System.ComponentModel.DataAnnotations;
using Prokast.Server.Entities;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductEdit
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string EAN { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
