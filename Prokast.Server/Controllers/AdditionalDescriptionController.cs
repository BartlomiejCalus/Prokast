using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalDescriptionResponseModels;
using Prokast.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("api/additionaldescriptions")]
    [Tags("Additional Descriptions")]
    public class AdditionalDescriptionController : ControllerBase
    {
        private readonly IAdditionalDescriptionService _additionalDescriptionService;

        public AdditionalDescriptionController(IAdditionalDescriptionService descriptionService)
        {
            _additionalDescriptionService = descriptionService;
        }
        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }

        [HttpPost]
        [EndpointSummary("Create an additional description")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates an additional description for a product.")]
        public ActionResult<Response> CreateAdditionalDescription([FromBody] AdditionalDescriptionCreateDto additionalDescription, [FromQuery] int regionID, [FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalDescriptionService.CreateAdditionalDescription(additionalDescription, clientIdFromToken, regionID, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [EndpointSummary("Get all additional descriptions")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all additional descriptions of all products assigned to the client.")]
        public ActionResult<Response> GetAllDescriptions()
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalDescriptionService.GetAllDescriptions(clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [EndpointSummary("Get an additional description")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific additional description.")]
        public ActionResult<Response> GetDescriptionByID([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalDescriptionService.GetDescriptionsByID(ID, clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Titles")]
        [EndpointSummary("Get additional descriptions by titles")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of additional descriptions with a title containing the given word.")]
        public ActionResult<Response> GetDescriptionsByNames([FromQuery] string Title)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalDescriptionService.GetDescriptionsByNames(Title, clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Region")]
        [EndpointSummary("Get additional descriptions by region")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of additional descriptions in the same region.")]
        public ActionResult<Response> GetDescriptionByRegion([FromQuery] int region)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalDescriptionService.GetDescriptionByRegion(region, clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Product")]
        [EndpointSummary("Get all additional descriptions in Product")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of additional descriptions that are components in the same product.")]
        public ActionResult<Response> GetAllDescriptionsInProduct([FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalDescriptionService.GetAllDescriptionsInProduct(clientIdFromToken, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{ID}")]
        [EndpointSummary("Edit an additional description")]
        [ProducesResponseType(typeof(AdditionalDescriptionEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given additional description.")]
        public ActionResult<Response> EditAdditionalDescription([FromRoute] int ID, [FromBody] AdditionalDescriptionCreateDto data)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _additionalDescriptionService.EditAdditionalDescription(clientIdFromToken, ID, data);
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
        [EndpointSummary("Delete an additional description")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given additional description")]
        public ActionResult<Response> DeleteDescription([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalDescriptionService.DeleteAdditionalDescription(clientIdFromToken, ID);
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