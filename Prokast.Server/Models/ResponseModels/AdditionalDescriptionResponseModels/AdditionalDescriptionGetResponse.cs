using Prokast.Server.Entities;

namespace Prokast.Server.Models.ResponseModels.AdditionalDescriptionResponseModels
{
    public class AdditionalDescriptionGetResponse : Response
    {
        public List<AdditionalDescription> Model { get; set; }
    }
}
