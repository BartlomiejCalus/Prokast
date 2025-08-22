using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class Photo 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }

        public required int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
