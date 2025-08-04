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
    [Tags("Photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        [EndpointSummary("Creates new photo")]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a new photo based on provided data.")]
        public ActionResult<Response> CreatePhoto([FromBody] PhotoDto photo,[FromQuery] int clientID)
        {
            try
            {
                var result = _photoService.CreatePhoto(photo, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex) {return BadRequest(ex); }  
        }

        [HttpGet]
        [EndpointSummary("Get all photos")]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all photos of the client's products.")]
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
        [EndpointSummary("Get a photo")]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific photo.")]
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

        [HttpGet("Product")]
        [EndpointSummary("Get all photos in product")]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllPhotosInProduct([FromQuery] int clientID, [FromQuery] int productID)
        {
            try
            {
                var result = _photoService.GetAllPhotosInProduct(clientID, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPut("{ID}")]
        [EndpointSummary("Edit a photo")]
        [ProducesResponseType(typeof(PhotoEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given ptoho.")]
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
        [EndpointSummary("Delete photo")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given photo.")]
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
