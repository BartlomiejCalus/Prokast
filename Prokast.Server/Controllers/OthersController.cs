using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Prokast.Server.Controllers
{
    [Authorize(Roles = "1,2,3,4,5,6")]
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
        public ActionResult<Response> GetMainPage()
        {
            var clientIdFromToken = GetClientIdFromToken();
            try
            {
                var result = _services.GetMainPage(clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }
    }
}
