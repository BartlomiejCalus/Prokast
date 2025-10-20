using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("api/params")]
    public class ParamsController: ControllerBase
    {
        private readonly IParamsService _paramsService;

        public ParamsController(IParamsService paramsService)
        {
            _paramsService = paramsService;
        }

        #region CreateCustomParam
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateCustonParam([FromBody] CustomParamsDto customParamsDto, [FromQuery] int clientID, [FromQuery] int regionID, [FromQuery] int productID)
        {
            try 
            {
                var result = _paramsService.CreateCustomParam(customParamsDto, clientID, regionID, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAllParams
        [HttpGet]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        #endregion

        #region GetParamsByID
        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        #endregion

        #region getParamsByName
        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

        #region EditParams
        [HttpPut("{ID}")]
        [ProducesResponseType(typeof(ParamsEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

        #region DeleteParams
        [HttpDelete("{ID}")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
