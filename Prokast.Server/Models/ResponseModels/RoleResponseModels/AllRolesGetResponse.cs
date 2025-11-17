using Prokast.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace Prokast.Server.Models.ResponseModels.RoleResponseModels
{
    public class AllRolesGetResponse:Response
    {
        [Required]
        public List<Role> Model { get; set; }
    }
}
