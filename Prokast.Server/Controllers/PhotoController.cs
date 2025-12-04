using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.PhotoModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.PhotoResponseModels;
using Prokast.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Prokast.Server.Controllers
{
    [Authorize(Roles = "1,2,3,5,6")]
    [Route("api/photos")]
    [Tags("Photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }

        [HttpPost("Product/{productID}")]
        [EndpointSummary("Creates new photo")]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a new photo based on provided data.")]
        public ActionResult<Response> CreatePhoto([FromBody] PhotoDto photo, [FromRoute] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _photoService.CreatePhoto(photo, clientIdFromToken, productID);
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
        public ActionResult<Response> GetAllPhotos()
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _photoService.GetAllPhotos(clientIdFromToken);
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
        public ActionResult<Response> GetPhotosByID([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _photoService.GetPhotosByID(clientIdFromToken, ID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Product/{productID}")]
        [EndpointSummary("Get all photos in product")]
        [ProducesResponseType(typeof(PhotoGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllPhotosInProduct([FromRoute] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _photoService.GetAllPhotosInProduct(clientIdFromToken, productID);
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
        public ActionResult<Response> EditPhotos([FromRoute] int ID, [FromBody] PhotoEdit data)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _photoService.EditPhotos(clientIdFromToken, ID, data);
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
        public ActionResult<Response> DeletePhotos([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _photoService.DeletePhotos(clientIdFromToken, ID);
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
