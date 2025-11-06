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
        public ActionResult<Response> CreateWarehouse([FromBody] StoredProductCreateMultipleDto storedProducts, [FromQuery] int warehouseID, [FromQuery] int clientID, [FromQuery] int productID)
        {
            try
            {
                var result = _storedProductService.CreateStoredProduct(storedProducts, warehouseID, clientID, productID);
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
        public ActionResult<Response> GetAllStoredProducts([FromQuery] int clientID,[FromQuery] int warehouseID)
        {
            try
            {
                var result = _storedProductService.GetAllStoredProducts(clientID, warehouseID);
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
        public ActionResult<Response> GetStoredProductByID([FromQuery] int clientID, [FromQuery] int warehouseID,[FromRoute] int ID)
        {
            try
            {
                var result = _storedProductService.GetStoredProductByID(clientID,warehouseID, ID);
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
        public ActionResult<Response> GetStoredProductsBelowMinimum([FromQuery] int clientID,[FromQuery] int warehouseID)
        {
            try
            {
                var result = _storedProductService.GetStoredProductsBelowMinimum(clientID, warehouseID);
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
        public ActionResult<Response> GetStoredProductBySKU([FromQuery] int clientID, [FromQuery] int warehouseID, [FromRoute] string SKU)
        {
            try
            {
                var result = _storedProductService.GetStoredProductsBySKU(clientID, warehouseID, SKU);
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
        public ActionResult<Response> GetStoredProductsMinimalData([FromQuery] int clientID, [FromQuery] int warehouseID)
        {
            try
            {
                var result = _storedProductService.GetStoredProductsMinimalData(clientID, warehouseID);
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
        [HttpPut("quantity/{ID}")]
        [ProducesResponseType(typeof(StoredProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditStoredProductQuantity([FromQuery] int clientID, [FromRoute] int ID, [FromQuery] int quantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _storedProductService.EditStoredProductQuantity(clientID, ID, quantity);
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
        public ActionResult<Response> EditStoredProductMinQuantity([FromQuery] int clientID, [FromRoute] int ID, [FromQuery] int minQuantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _storedProductService.EditStoredProductMinQuantity(clientID, ID, minQuantity);
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
        public ActionResult<Response> EditMultipleStoredProductMinQuantity([FromQuery]int clientID,[FromBody] List<EditMultipleStoredProductMinQuantityDto> listToEdit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _storedProductService.EditMultipleStoredProductMinQuantity(clientID, listToEdit);
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
        public ActionResult<Response> DeleteStoredProduct([FromQuery] int clientID, [FromRoute] int ID)
        {

            try
            {
                var result = _storedProductService.DeleteStoredProduct(clientID, ID);
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
