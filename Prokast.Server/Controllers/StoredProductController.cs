using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.StoredProductModels;
using Prokast.Server.Models.ResponseModels.StoredProductResponseModels;
using Prokast.Server.Entities;
using Prokast.Server.Models.ClientModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;



namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("api/storedproducts")]
    public class StoredProductController: ControllerBase
    {
        private readonly IStoredProductService _storedProductService;

        public StoredProductController(IStoredProductService storedProductService)
        {
            _storedProductService = storedProductService;
        }

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateWarehouse([FromBody] StoredProductCreateMultipleDto storedProducts, [FromQuery] int warehouseID, [FromQuery] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _storedProductService.CreateStoredProduct(storedProducts, warehouseID, clientIdFromToken, productID);
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
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllStoredProducts([FromQuery] int warehouseID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _storedProductService.GetAllStoredProducts(clientIdFromToken, warehouseID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetStoredProductByID([FromQuery] int warehouseID,[FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _storedProductService.GetStoredProductByID(clientIdFromToken, warehouseID, ID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("below")]
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetStoredProductsBelowMinimum([FromQuery] int warehouseID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _storedProductService.GetStoredProductsBelowMinimum(clientIdFromToken, warehouseID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("SKU/{SKU}")]
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetStoredProductBySKU([FromQuery] int warehouseID, [FromRoute] string SKU)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _storedProductService.GetStoredProductsBySKU(clientIdFromToken, warehouseID, SKU);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Minimal")]
        [ProducesResponseType(typeof(StoredProductGetMinimalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetStoredProductsMinimalData([FromQuery] int warehouseID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _storedProductService.GetStoredProductsMinimalData(clientIdFromToken, warehouseID);
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
        [ProducesResponseType(typeof(StoredProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditStoredProductQuality([FromRoute] int ID, [FromQuery] int quantity)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _storedProductService.EditStoredProductQuantity(clientIdFromToken, ID, quantity);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("minquantity/{ID}")]
        [ProducesResponseType(typeof(StoredProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditStoredProductMinQuality([FromRoute] int ID, [FromQuery] int minQuantity)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _storedProductService.EditStoredProductMinQuantity(clientIdFromToken, ID, minQuantity);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("minquantity/multiple")]
        [ProducesResponseType(typeof(StoredProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditMultipleStoredProductMinQuantity([FromBody] List<EditMultipleStoredProductMinQuantityDto> listToEdit)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _storedProductService.EditMultipleStoredProductMinQuantity(clientIdFromToken, listToEdit);
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
        public ActionResult<Response> DeleteStoredProduct([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _storedProductService.DeleteStoredProduct(clientIdFromToken, ID);
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
