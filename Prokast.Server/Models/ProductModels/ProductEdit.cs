using System.ComponentModel.DataAnnotations;
using Prokast.Server.Entities;

namespace Prokast.Server.Models.ProductModels
{
    public class ProductEdit
    {

        public string? Name { get; set; }
        public string? SKU { get; set; }
        public string? EAN { get; set; }
        public string? Description { get; set; }

    }
}
