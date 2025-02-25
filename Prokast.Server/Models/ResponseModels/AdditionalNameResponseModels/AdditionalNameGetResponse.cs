using Prokast.Server.Entities;

namespace Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels
{
    public class AdditionalNameGetResponse : Response
    {
        public List<AdditionalName> Model { get; set; }
    }
}
