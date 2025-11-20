
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalDescriptionResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using System.Security.Claims;


namespace Prokast.Server.Controllers
{
    [Authorize(Roles = "1,2,3,5")]
    [Route("api/addName")]
    public class AdditionalNameController : ControllerBase
    {
        private readonly IAdditionalNameService _additionalNameService;

        public AdditionalNameController(IAdditionalNameService additionalNameService)
        {
            _additionalNameService = additionalNameService;
        }

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateAdditionalName([FromBody] AdditionalNameDto additionalNameDto, [FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalNameService.CreateAdditionalName(additionalNameDto, clientIdFromToken, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       

       
        [HttpGet]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllNames()
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalNameService.GetAllNames(clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        
        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetNamesByID([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalNameService.GetNamesByID( ID, clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Title/{ID}")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetNamesByIDNames([FromRoute] int ID, [FromQuery] string Title) 
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalNameService.GetNamesByIDNames(ID, Title , clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        [HttpGet("Region/{ID}")]
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetNamesByIDRegion([FromRoute] int ID, [FromQuery] int Region)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalNameService.GetNamesByIDRegion(ID, Region, clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Product")]
        [EndpointSummary("Get all additional names in Product")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of additional names that are components in the same product.")]
        public ActionResult<Response> GetAllNamesInProduct([FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalNameService.GetAllNamesInProduct(clientIdFromToken, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region EditParams
        [HttpPut("{ID}")]
        [ProducesResponseType(typeof(AdditionalNameEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditAdditionalName([FromRoute] int ID, [FromBody]  AdditionalNameDto data) 
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _additionalNameService.EditAdditionalName(clientIdFromToken, ID, data);
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
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> DeleteParams([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _additionalNameService.DeleteAdditionalName(clientIdFromToken, ID);
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