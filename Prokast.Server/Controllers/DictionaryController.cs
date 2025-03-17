using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.DictionaryParamsResponseModels;
using Prokast.Server.Services.Interfaces;


namespace Prokast.Server.Controllers
{
    [Route("api/dictionary")]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _paramsService;

        public DictionaryController(IDictionaryService paramsService)
        {
            _paramsService = paramsService;
        }

        #region GetAllParams
        [HttpGet]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllParams()
        {
            try
            {
                var lista = _paramsService.GetAllParams();
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetParamsByID
        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetParamsByID( [FromRoute] int ID)
        {
            try
            {
                var param = _paramsService.GetParamsByID(ID);
                if (param is ErrorResponse) return BadRequest(param);
                return Ok(param);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region getParamsByName
        [HttpGet("DictionaryName/{DictionaryName}")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetParamsByName( [FromRoute] string name)
        {
            try
            {
                var param = _paramsService.GetParamsByName(name);
                if (param is ErrorResponse) return BadRequest(param);
                return Ok(param);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region ReturningValuesByName
        [HttpGet("Region/{Region}")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetParamsByRegion ( [FromRoute] int region)
        {
            try
            {
                var param = _paramsService.GetParamsByRegion(region);
                if (param is ErrorResponse) return BadRequest(param);
                return Ok(param);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Name/{Name}")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetValuesByName ( [FromRoute] string name)
        {
            try
            {
                var param = _paramsService.GetValuesByName(name);
                if (param is ErrorResponse) return BadRequest(param);
                return Ok(param);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
