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
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace Prokast.Server.Controllers
{
    [Authorize(Roles = "1,2,3,4,5")]
    [Route("api/priceLists")]
    public class PricesController : ControllerBase
    {
        private readonly IPricesService _priceService;
        public PricesController(IPricesService pricesService)
        {
            _priceService = pricesService;
        }

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreatePriceList([FromBody] PriceListsCreateDto priceLists, [FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _priceService.CreatePriceList(priceLists, clientIdFromToken, productID);
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
        public ActionResult<Response> CreatePrice([FromBody] PricesDto prices, [FromRoute] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _priceService.CreatePrice(prices, productID, clientIdFromToken);
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
        public ActionResult<Response> GetAllPriceLists()
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var lista = _priceService.GetAllPriceLists(clientIdFromToken);
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
        public ActionResult<Response> GetPriceListsByName([FromRoute] string name)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var lista = _priceService.GetPriceListsByName(clientIdFromToken, name);
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
        public ActionResult<Response> GetAllPrices([FromRoute] int priceListID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var lista = _priceService.GetAllPrices(clientIdFromToken, priceListID);
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
        public ActionResult<Response> GetPricesByRegion ([FromRoute]int priceListID, [FromQuery]int regionID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var lista = _priceService.GetPricesByRegion(clientIdFromToken, priceListID, regionID);
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
        public ActionResult<Response> GetPricesByName([FromRoute] int priceListID, [FromQuery] string name)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var lista = _priceService.GetPricesByName(clientIdFromToken, priceListID, name);
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
        public ActionResult<Response> GetAllParamsInProduct([FromQuery] int priceListID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _priceService.GetAllPricesInProduct(clientIdFromToken, priceListID);
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
        public ActionResult<Response> EditPrice(EditPriceDto editPriceDto, [FromQuery] int priceListID,[FromRoute] int priceID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _priceService.EditPrice(editPriceDto, clientIdFromToken, priceListID, priceID);
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
        public ActionResult<Response> DeletePrice([FromQuery] int priceListID, [FromRoute] int priceID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _priceService.DeletePrice(clientIdFromToken, priceListID, priceID);
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
        public ActionResult<Response> DeletePriceList([FromRoute] int priceListID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _priceService.DeletePriceList(clientIdFromToken, priceListID);
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
