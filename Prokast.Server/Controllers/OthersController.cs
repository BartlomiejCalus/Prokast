using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
    }
}
