using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using System.ComponentModel.DataAnnotations;



namespace Prokast.Server.Models.ResponseModels.PhotoResponseModels
{
    public class PhotoGetResponse : Response
    {
        [Required]
        public List<Photo> Model { get; set; }
    }
}
