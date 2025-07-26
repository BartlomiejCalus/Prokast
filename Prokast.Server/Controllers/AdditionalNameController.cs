
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels;
using Prokast.Server.Services.Interfaces;


namespace Prokast.Server.Controllers
{
    [Route("api/addName")]
    [Tags("Additional Names")]
    public class AdditionalNameController : ControllerBase
    {
        private readonly IAdditionalNameService _additionalNameService;

        public AdditionalNameController(IAdditionalNameService additionalNameService)
        {
            _additionalNameService = additionalNameService;
        }

        
        [HttpPost]
        [EndpointSummary("Create an additional name")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates an additional name for a product.")]
        public ActionResult<Response> CreateAdditionalName([FromBody] AdditionalNameDto additionalNameDto, [FromQuery] int clientID)
        {
            try
            {
                var result = _additionalNameService.CreateAdditionalName(additionalNameDto, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region GetParams

        [HttpGet]
        [EndpointSummary("Get all additional names")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all additional names of all products assigned to the client.")]
        public ActionResult<Response> GetAllNames([FromQuery] int clientID)
        {
            try
            {
                var result = _additionalNameService.GetAllNames(clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        
        [HttpGet("{ID}")]
        [EndpointSummary("Get an additional name")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific additional name.")]
        public ActionResult<Response> GetNamesByID([FromRoute] int ID, [FromQuery] int clientID )
        {
            try
            {
                var result = _additionalNameService.GetNamesByID( ID, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Title/{ID}")]
        [EndpointSummary("Get additional names by titles")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of additional names with a given title.")]

        public ActionResult<Response> GetNamesByIDNames([FromRoute] int ID, [FromQuery] string Title, [FromQuery] int clientID) 
        {
            try
            {
                var result = _additionalNameService.GetNamesByIDNames(ID, Title ,clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        [HttpGet("Region/{ID}")]
        [EndpointSummary("Get additional names by region")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of additional names in the same region.")]
        public ActionResult<Response> GetNamesByIDRegion([FromRoute] int ID, [FromQuery] int Region, [FromQuery] int clientID)
        {
            try
            {
                var result = _additionalNameService.GetNamesByIDRegion(ID, Region, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Product")]
        [EndpointSummary("Get all additional names in product")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllNamesInProduct([FromQuery]int clientID, [FromQuery]int productID)
        {
            try
            {
                var result = _additionalNameService.GetAllNamesInProduct(clientID, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region EditParams
        [HttpPut("{ID}")]
        [EndpointSummary("Edit an additional name")]
        [ProducesResponseType(typeof(AdditionalNameEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given additional name.")]
        public ActionResult<Response> EditAdditionalName([FromQuery] int clientID, [FromRoute] int ID, [FromBody]  AdditionalNameDto data) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _additionalNameService.EditAdditionalName(clientID, ID, data);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region DeleteParams
        [HttpDelete("{ID}")]
        [EndpointSummary("Delete an additional name")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given additional name")]
        public ActionResult<Response> DeleteParams([FromQuery] int clientID, [FromRoute] int ID)
        {

            try
            {
                var result = _additionalNameService.DeleteAdditionalName(clientID, ID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion



    }
}