using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.PricesModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PriceModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Models.ResponseModels.PriceResponseModels.PriceListResponseModels;




namespace Prokast.Server.Controllers
{
    [Route("api/priceLists")]
    [Tags("Pricelists and prices")]
    public class PricesController : ControllerBase
    {
        private readonly IPricesService _priceService;
        public PricesController(IPricesService pricesService)
        {
            _priceService = pricesService;
        }

        [HttpPost]
        [EndpointSummary("Create a pricelist")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a pricelist for a product.")]
        public ActionResult<Response> CreatePriceList([FromBody] PriceListsCreateDto priceLists, [FromQuery] int clientID)
        {
            try
            {
                var result = _priceService.CreatePriceList(priceLists, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{priceListID}")]
        [EndpointSummary("Create a price")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a price inside a given pricelist.")]
        public ActionResult<Response> CreatePrice([FromBody] PricesDto prices, [FromRoute] int priceListID, [FromQuery] int clientID)
        {
            try
            {
                var result = _priceService.CreatePrice(prices, priceListID, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [EndpointSummary("Get all pricelists")]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all pricelists of all products assigned to the client.")]
        public ActionResult<Response> GetAllPriceLists([FromQuery]int clientID)
        {
            try
            {
                var lista = _priceService.GetAllPriceLists(clientID);
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{name}")]
        [EndpointSummary("Get pricelists by name")]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of pricelists with a name containing the given word.")]
        public ActionResult<Response> GetPriceListsByName([FromQuery] int clientID, [FromRoute] string name)
        {
            try
            {
                var lista = _priceService.GetPriceListsByName(clientID, name);
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Product")]
        [EndpointSummary("Get all prices in product")]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllPricesInProduct([FromQuery] int clientID, [FromQuery] int productID)
        {
            try
            {
                var lista = _priceService.GetAllPricesInProduct(clientID, productID);
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("prices/{priceListID}")]
        [EndpointSummary("Get all prices")]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all prices in a given pricelist.")]
        public ActionResult<Response> GetAllPrices([FromQuery] int clientID, [FromRoute] int priceListID)
        {
            try
            {
                var lista = _priceService.GetAllPrices(clientID, priceListID);
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("prices/region/{priceListID}")]
        [EndpointSummary("Get prices by region")]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all prices in a given pricelist in the same region.")]
        public ActionResult<Response> GetPricesByRegion ([FromQuery] int clientID, [FromRoute]int priceListID, [FromQuery]int regionID)
        {
            try
            {
                var lista = _priceService.GetPricesByRegion(clientID, priceListID, regionID);
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("prices/name/{priceListID}")]
        [EndpointSummary("Get prices by name")]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all prices in a given pricelist with a name containing a given word.")]
        public ActionResult<Response> GetPricesByName([FromQuery] int clientID, [FromRoute] int priceListID, [FromQuery] string name)
        {
            try
            {
                var lista = _priceService.GetPricesByName(clientID, priceListID, name);
                if (lista is ErrorResponse) return BadRequest(lista);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("prices/{priceID}")]
        [EndpointSummary("Edit price")]
        [ProducesResponseType(typeof(ParamsEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given price.")]
        public ActionResult<Response> EditPrice(EditPriceDto editPriceDto, [FromQuery] int clientID, [FromQuery] int priceListID,[FromRoute] int priceID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _priceService.EditPrice(editPriceDto, clientID, priceListID, priceID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("prices/{priceID}")]
        [EndpointSummary("Delete price")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given price.")]
        public ActionResult<Response> DeletePrice([FromQuery] int clientID, [FromQuery] int priceListID, [FromRoute] int priceID)
        {
            try
            {
                var result = _priceService.DeletePrice(clientID, priceListID, priceID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{priceListID}")]
        [EndpointSummary("Delete pricelist")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given pricelist.")]
        public ActionResult<Response> DeletePriceList([FromQuery] int clientID, [FromRoute] int priceListID)
        {
            try
            {
                var result = _priceService.DeletePriceList(clientID, priceListID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
