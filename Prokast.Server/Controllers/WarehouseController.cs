using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.ResponseModels.WarehouseResponseModels;
using Prokast.Server.Models.WarehouseModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("Api/Warehouses")]
    public class WarehouseController: ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateWarehouse([FromBody] WarehouseCreateDto warehouseCreateDto, [FromQuery] int clientID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.CreateWarehouse(warehouseCreateDto, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get
        [HttpGet]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllWarehouses([FromQuery] int clientID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.GetAllWarehouses(clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetWarehouseByID([FromQuery] int clientID, [FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.GetWarehouseById(clientID, ID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Name/{name}")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetWarehousesByName([FromQuery] int clientID, [FromRoute] string name)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.GetWarehousesByName(clientID, name);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("City/{city}")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetWarehousesByCity([FromQuery] int clientID, [FromRoute] string city)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.GetWarehousesByCity(clientID, city);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Country/{country}")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetWarehousesByCountry([FromQuery] int clientID, [FromRoute] string country)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.GetWarehouseByCountry(clientID, country);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Minimal")]
        [ProducesResponseType(typeof(WarehouseGetMinimalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetWarehousesMinimalData([FromQuery] int clientID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.GetWarehousesMinimalData(clientID);
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
        [ProducesResponseType(typeof(WarehouseEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditWarehouse([FromQuery] int clientID, [FromRoute] int ID, [FromBody] WarehouseCreateDto warehouseCreateDto)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _warehouseService.EditWarehouse(clientID, ID, warehouseCreateDto);
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

        #region Delete
        [HttpDelete("{ID}")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> DeleteWarehouse([FromQuery] int clientID, [FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (clientIdFromToken != clientID)
                return Forbid();

            try
            {
                var result = _warehouseService.DeleteWarehouse(clientID, ID);
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

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }
    }
}
