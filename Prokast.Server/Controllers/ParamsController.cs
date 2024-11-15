﻿using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Services;
using Prokast.Server.Models;


namespace Prokast.Server.Controllers
{
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
        public ActionResult<Response> CreateCustonParam([FromBody] CustomParamsDto customParamsDto, [FromQuery] int clientID)
        {
            try 
            {
                var result = _paramsService.CreateCustomParam(customParamsDto, clientID);
                if (result.Model is string) return BadRequest(result);
                return Created();
            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAllParams
        [HttpGet]
        public ActionResult<Response> GetAllParams([FromQuery] int clientID) 
        {
            try
            {
                var result = _paramsService.GetAllParams(clientID);
                if (result.Model is string) return BadRequest(result);
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
        public ActionResult<Response> GetParamsByID([FromQuery] int clientID, [FromRoute] int ID)
        {
            try
            {
                var result = _paramsService.GetParamsByID(clientID, ID);
                if (result.Model is string) return BadRequest(result);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region getParamsByName
        [HttpGet("name/{name}")]
        public ActionResult<Response> GetParamsByName([FromQuery] int clientID, [FromRoute] string name)
        {
            try
            {
                var result = _paramsService.GetParamsByName(clientID, name);
                if (result.Model is string) return BadRequest(result);
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
        public ActionResult<Response> EditParams([FromQuery] int clientID, [FromRoute] int ID, [FromBody] CustomParamsDto data)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _paramsService.EditParams(clientID, ID, data);
                if (result.Model is string) return BadRequest(result);

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
        public ActionResult<Response> DeleteParams([FromQuery] int clientID, [FromRoute] int ID)
        {
            
            try
            {
                var result = _paramsService.DeleteParams(clientID, ID);
                if (result.Model is string) return BadRequest(result);

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
