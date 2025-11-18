using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalDescriptionResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using System.Security.Claims;


namespace Prokast.Server.Controllers
{
    [Authorize(Roles = "1,2,3,5")]
    [Route("api/params")]
    public class ParamsController: ControllerBase
    {
        private readonly IParamsService _paramsService;

        public ParamsController(IParamsService paramsService)
        {
            _paramsService = paramsService;
        }

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateCustonParam([FromBody] CustomParamsDto customParamsDto, [FromQuery] int regionID, [FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try 
            {
                var result = _paramsService.CreateCustomParam(customParamsDto, clientIdFromToken, regionID, productID);
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
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllParams() 
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _paramsService.GetAllParams(clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetParamsByID([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _paramsService.GetParamsByID(clientIdFromToken, ID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetParamsByName([FromRoute] string name)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _paramsService.GetParamsByName(clientIdFromToken, name);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
        

        [HttpGet("Product")]
        [EndpointSummary("Get all custom params in Product")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of custom parameters that are components in the same product.")]
        public ActionResult<Response> GetAllParamsInProduct([FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _paramsService.GetAllParamsInProduct(clientIdFromToken, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Edit
        [HttpPut("{ID}")]
        [ProducesResponseType(typeof(ParamsEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditParams([FromRoute] int ID, [FromBody] CustomParamsDto data)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid) 
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _paramsService.EditParams(clientIdFromToken, ID, data);
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
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> DeleteParams([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _paramsService.DeleteParams(clientIdFromToken, ID);
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
