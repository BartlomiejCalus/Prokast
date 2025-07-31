using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.StoredProductModels;
using Prokast.Server.Models.ResponseModels.StoredProductResponseModels;
using Prokast.Server.Entities;
using Prokast.Server.Models.ClientModels;


namespace Prokast.Server.Controllers
{
    /// <summary>
    /// Poniższe funkcje odnoszą sie tylko do produktów w wybranym magazynie
    /// </summary>
    [Route("api/storedproducts")]
    [Tags("[Warehouse] Stored Products")]
    public class StoredProductController: ControllerBase
    {
        private readonly IStoredProductService _storedProductService;

        public StoredProductController(IStoredProductService storedProductService)
        {
            _storedProductService = storedProductService;
        }

        #region Create
        [HttpPost]
        [EndpointSummary("Create a stored product")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a product stored in a given warehouse.")]
        public ActionResult<Response> CreateStoredProduct([FromBody] StoredProductCreateMultipleDto storedProducts, [FromQuery] int warehouseID, [FromQuery] int clientID)
        {
            try
            {
                var result = _storedProductService.CreateStoredProduct(storedProducts, warehouseID, clientID);
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
        [EndpointSummary("Get all stored products")]
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all products stored in a given warehouse.")]
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
        [EndpointSummary("Get a stored product")]
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific product stored in a given warehouse.")]
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
        [EndpointSummary("Get stored products below minimum")]
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all products stored in a given warehouse below their minimum quantity value.")]
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
        [EndpointSummary("Get a stored product by SKU")]
        [ProducesResponseType(typeof(StoredProductGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific product stored in a given warehouse by the given SKU.")]
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
        [EndpointSummary("Get all stored products (minimal data)")]
        [ProducesResponseType(typeof(StoredProductGetMinimalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns minimal data of all products stored in a gicen warehouse.")]
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
        [HttpPut("{ID}")]
        [EndpointSummary("Edit stored product's quaitity")]
        [ProducesResponseType(typeof(StoredProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits the quantity of the given stored product.")]
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
        [EndpointSummary("Edit stored product's minimum quantity")]
        [ProducesResponseType(typeof(StoredProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits the minimum quantity of a given stored product.")]
        public ActionResult<Response> EditStoredProductMinQuality([FromQuery] int clientID, [FromRoute] int ID, [FromQuery] int minQuantity)
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
        [EndpointSummary("Edit multiple stored products minimum quantity")]
        [ProducesResponseType(typeof(StoredProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits the minimum quantity of multiple given stored products.")]
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
        [EndpointSummary("Delete a stored product")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a product stored in a given warehouse.")]
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
