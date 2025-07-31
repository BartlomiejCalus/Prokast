using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models
{
    public class EmailMessage
    {
        [Required]
        public List<string> To { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public List<(string, byte[])>? Attachments { get; set; }
    }
}
