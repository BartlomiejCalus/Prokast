using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Prokast.Server.Models
{
    public class Response
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        
    }
}
