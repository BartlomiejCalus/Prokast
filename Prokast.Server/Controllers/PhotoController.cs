using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.PhotoResponseModels;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Controllers
{
    [Route("api/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllPhotos([FromQuery] int clientID)
        {
            try
            {
                var result = _photoService.GetAllPhotos(clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetPhotosByID([FromQuery] int clientID, [FromRoute] int ID)
        {
            try
            {
                var result = _photoService.GetPhotosByID(clientID, ID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut("{ID}")]
        [ProducesResponseType(typeof(PhotoEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditPhotos([FromQuery] int clientID, [FromRoute] int ID, [FromBody] PhotoEdit data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _photoService.EditPhotos(clientID, ID, data);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> DeletePhotos([FromQuery] int clientID, [FromRoute] int ID)
        {

            try
            {
                var result = _photoService.DeletePhotos(clientID, ID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
