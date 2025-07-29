using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.ResponseModels.WarehouseResponseModels;
using Prokast.Server.Models.WarehouseModels;

namespace Prokast.Server.Controllers
{
    [Route("Api/Warehouses")]
    [Tags("[Warehouse] Warehouses")]
    public class WarehouseController: ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        #region Create
        [HttpPost]
        [EndpointSummary("Create a warehouse")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a warehouse for the client.")]
        public ActionResult<Response> CreateWarehouse([FromBody] WarehouseCreateDto warehouseCreateDto, [FromQuery] int clientID)
        {
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
        [EndpointSummary("Get all warehouses")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all warehouses assigned to the client.")]
        public ActionResult<Response> GetAllWarehouses([FromQuery] int clientID)
        {
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
        [EndpointSummary("Get a warehouse")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific warehouse assigned to the client.")]
        public ActionResult<Response> GetWarehouseByID([FromQuery] int clientID, [FromRoute] int ID)
        {
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
        [EndpointSummary("Get warehouses by name")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all warehouses assigned to the client with a name containing the given word")]
        public ActionResult<Response> GetWarehousesByName([FromQuery] int clientID, [FromRoute] string name)
        {
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
        [EndpointSummary("Get all warehouses in the city")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operatio. Endpoint returns all warehouses assigned to the client in th given city.")]
        public ActionResult<Response> GetWarehousesByCity([FromQuery] int clientID, [FromRoute] string city)
        {
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
        [EndpointSummary("Get all warehouses in the country")]
        [ProducesResponseType(typeof(WarehouseGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all warehouses assigned to the client in the given country.")]
        public ActionResult<Response> GetWarehousesByCountry([FromQuery] int clientID, [FromRoute] string country)
        {
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

        /// <summary>
        /// Minimal data oznacza że wyświetlane są tylko najwązniejsze dane istotne dla klienta do którego należy magazyn(-y)
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        [HttpGet("Minimal")]
        [EndpointSummary("Get all warehouses (minimal data)")]
        [ProducesResponseType(typeof(WarehouseGetMinimalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns minimal data of all warehouses assigned to the client.")]
        public ActionResult<Response> GetWarehousesMinimalData([FromQuery] int clientID)
        {
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
        [EndpointSummary("Edit a warehouse")]
        [ProducesResponseType(typeof(WarehouseEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given warehouse.")]
        public ActionResult<Response> EditWarehouse([FromQuery] int clientID, [FromRoute] int ID, [FromBody] WarehouseCreateDto warehouseCreateDto)
        {
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
        [EndpointSummary("Detete a warehouse")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given warehouse and all products stored in it.")]
        public ActionResult<Response> DeleteWarehouse([FromQuery] int clientID, [FromRoute] int ID)
        {

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
    }
}
