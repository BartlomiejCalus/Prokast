using Prokast.Server.Entities;

namespace Prokast.Server.Models.ResponseModels.DictionaryParamsResponseModels
{
    public class DictionaryGetResponse : Response
    {
        public List<DictionaryParams> Model { get; set; }
    }
}
