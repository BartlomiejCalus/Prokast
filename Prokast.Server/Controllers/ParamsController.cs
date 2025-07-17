using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Services.Interfaces;


namespace Prokast.Server.Controllers
{
    [Route("api/params")]
    [Tags("Custom Parameters")]
    public class ParamsController: ControllerBase
    {
        private readonly IParamsService _paramsService;

        public ParamsController(IParamsService paramsService)
        {
            _paramsService = paramsService;
        }

        #region Create
        [HttpPost]
        [EndpointSummary("Create a custom parameter")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a custom parameter for a product.")]
        public ActionResult<Response> CreateCustonParam([FromBody] CustomParamsDto customParamsDto, [FromQuery] int clientID)
        {
            try 
            {
                var result = _paramsService.CreateCustomParam(customParamsDto, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get
        [HttpGet]
        [EndpointSummary("Get all custom parameters")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all custom parameters made by the client.")]
        public ActionResult<Response> GetAllParams([FromQuery] int clientID) 
        {
            try
            {
                var result = _paramsService.GetAllParams(clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{ID}")]
        [EndpointSummary("Get a custom parameter")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific custom parameter.")]
        public ActionResult<Response> GetParamsByID([FromQuery] int clientID, [FromRoute] int ID)
        {
            try
            {
                var result = _paramsService.GetParamsByID(clientID, ID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("name/{name}")]
        [EndpointSummary("Get custom parameters by name")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all custom parameters with a name containing a given word.")]
        public ActionResult<Response> GetParamsByName([FromQuery] int clientID, [FromRoute] string name)
        {
            try
            {
                var result = _paramsService.GetParamsByName(clientID, name);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
        #endregion

        #region Edit
        [HttpPut("{ID}")]
        [EndpointSummary("Edit a custom parameter")]
        [ProducesResponseType(typeof(ParamsEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given custom parameter.")]
        public ActionResult<Response> EditParams([FromQuery] int clientID, [FromRoute] int ID, [FromBody] CustomParamsDto data)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _paramsService.EditParams(clientID, ID, data);
                if (result is ErrorResponse) return BadRequest(result);

                if (result==null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{ID}")]
        [EndpointSummary("Delete a custom parameter")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given custom parameter.")]
        public ActionResult<Response> DeleteParams([FromQuery] int clientID, [FromRoute] int ID)
        {
            
            try
            {
                var result = _paramsService.DeleteParams(clientID, ID);
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
