using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Services;
using Prokast.Server.Models;


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
        public ActionResult<Response> GetAllParams()
        {
            try
            {
                var lista = _paramsService.GetAllParams();
                if (lista.Model is string) return BadRequest(lista);
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
        public ActionResult<Response> GetParamsByID( [FromRoute] int ID)
        {
            try
            {
                var param = _paramsService.GetParamsByID(ID);
                if (param.Model is string) return BadRequest(param);
                return Ok(param);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region getParamsByName
        [HttpGet("/DictionaryName/{name}")]
        public ActionResult<Response> GetParamsByName( [FromRoute] string name)
        {
            try
            {
                var param = _paramsService.GetParamsByName(name);
                if (param.Model is string) return BadRequest(param);
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
