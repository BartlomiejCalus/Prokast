namespace Prokast.Server.Models.ProductModels.ProductGetComponentsModels
{
    public class CustomParamsProductGetDto
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Value { get; set; }
        public required int RegionID { get; set; }
    }
}
