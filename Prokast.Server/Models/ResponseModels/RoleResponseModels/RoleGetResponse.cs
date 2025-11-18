using Prokast.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.ResponseModels.RoleResponseModels
{
    public class RoleGetResponse: Response
    {
        [Required]
        public Role Model { get; set; }
    }
}
