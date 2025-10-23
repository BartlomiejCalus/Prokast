using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.PriceModels;
using Prokast.Server.Models.PriceModels.PriceListModels;
using Prokast.Server.Models.PricesModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AdditionalDescriptionResponseModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Models.ResponseModels.PriceResponseModels.PriceListResponseModels;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("api/priceLists")]
    public class PricesController : ControllerBase
    {
        private readonly IPricesService _priceService;
        public PricesController(IPricesService pricesService)
        {
            _priceService = pricesService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreatePriceList([FromBody] PriceListsCreateDto priceLists, [FromQuery] int clientID, [FromQuery] int productID)
        {
            try
            {
                var result = _priceService.CreatePriceList(priceLists, clientID, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("{productID}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreatePrice([FromBody] PricesDto prices, [FromRoute] int productID, [FromQuery] int clientID)
        {
            try
            {
                var result = _priceService.CreatePrice(prices, productID, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [HttpGet("prices/{priceListID}")]
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(PriceListsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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

        [HttpGet("Product")]
        [EndpointSummary("Get all prices in Product")]
        [ProducesResponseType(typeof(AdditionalDescriptionGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a list of prices that are components in the same product.")]
        public ActionResult<Response> GetAllParamsInProduct([FromQuery] int clientID, [FromQuery] int priceListID)
        {
            try
            {
                var result = _priceService.GetAllPricesInProduct(clientID, priceListID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("prices/{priceID}")]
        [ProducesResponseType(typeof(ParamsEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
