using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class DictionaryParams
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public int IdRegion { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Value { get; set; }  
        [Required]
        public int OptionID { get; set; }
    }
}