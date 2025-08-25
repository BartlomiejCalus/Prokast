using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Entities
{
    public class DictionaryParams
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Value { get; set; }
        public required int OptionID { get; set; }

        public required int RegionID { get; set; }
        public virtual Region Regions { get; set; }
    }
}