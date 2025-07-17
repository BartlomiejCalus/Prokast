using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.DictionaryParamsResponseModels;
using Prokast.Server.Services.Interfaces;


namespace Prokast.Server.Controllers
{
    [Route("api/dictionary")]
    [Tags("Dictionary Parameters")]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _paramsService;

        public DictionaryController(IDictionaryService paramsService)
        {
            _paramsService = paramsService;
        }

        #region GetParams
        [HttpGet]
        [EndpointSummary("Get all dictionary parameters")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all dictionary parameters.")]
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

        [HttpGet("{ID}")]
        [EndpointSummary("Get a dictionary parameter")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific dictionary parameter.")]
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



        [HttpGet("DictionaryName/{name}")]
        [EndpointSummary("Get dictionary parameters by name")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of dictionary parameters sharing a given name.")]
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

        [HttpGet("Region/{region}")]
        [EndpointSummary("Get dictionary parameters by region")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of dictionary parameters in the same region.")]
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

        [HttpGet("Name/{name}")]
        [EndpointSummary("Get dictionary parameters by value")]
        [ProducesResponseType(typeof(DictionaryGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription(" a GET operation. Endpoint returns a list of values of a dictionary parameter having a given name.")]
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
