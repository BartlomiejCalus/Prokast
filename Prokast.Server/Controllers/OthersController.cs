using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("api/others")]
    public class OthersController: ControllerBase
    {
        private readonly IOthersService _services;

        public OthersController(IOthersService othersService)
        {
            _services = othersService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RegionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetRegions()
        {
            try
            {
                var lista = _services.GetRegions();
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("MainPage")]
        [ProducesResponseType(typeof(MainPageGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetMainPage([FromQuery] int clientID)
        {
            try
            {
                var result = _services.GetMainPage(clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
