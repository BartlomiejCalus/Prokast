
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalNameResponseModels;
using Prokast.Server.Services.Interfaces;


namespace Prokast.Server.Controllers
{
    [Route("api/addName")]
    public class AdditionalNameController : ControllerBase
    {
        private readonly IAdditionalNameService _additionalNameService;

        public AdditionalNameController(IAdditionalNameService additionalNameService)
        {
            _additionalNameService = additionalNameService;
        }

        
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateAdditionalName([FromBody] AdditionalNameDto additionalNameDto, [FromQuery] int clientID, [FromQuery] int regionID, [FromQuery] int productID)
        {
            try
            {
                var result = _additionalNameService.CreateAdditionalName(additionalNameDto, clientID, regionID, productID);
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
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(AdditionalNameGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

        #region EditParams
        [HttpPut("{ID}")]
        [ProducesResponseType(typeof(AdditionalNameEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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