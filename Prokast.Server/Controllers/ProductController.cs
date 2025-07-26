using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ProductModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.ProductResponseModels;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Controllers
{
    [Route("api/products")]
    [Tags("Products")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Create
        [HttpPost]
        [EndpointSummary("Create a product")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint creates a product.")]
        public ActionResult<Response> CreateProduct([FromBody] ProductCreateDto productCreateDto, [FromQuery] int clientID)
        {
            try
            {
                var result = _productService.CreateProduct(productCreateDto, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpPost("Get")]
        [EndpointSummary("Get all products")]
        [ProducesResponseType(typeof(ProductsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint returns all products assigned to the client. it can also filter the list by various criteria before returning it.")]
        public ActionResult<Response> GetProducts([FromBody] ProductGetFilter filter, [FromQuery]int clientID)
        {
            try
            {
                var result = _productService.GetProducts(filter, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/List")]
        [EndpointSummary("Get minimal data of filtered products")]
        [ProducesResponseType(typeof(ProductGetMinResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetProductsFromPath([FromQuery]int clientID, [FromQuery] string name, [FromQuery] string sku)
        {
            try
            {
                var result = _productService.GetProductsFromPath(clientID, name, sku);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("products/{productID}")]
        [EndpointSummary("Delete a product")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a product and, (in progress!) by extension, all other things attachted to it.")]
        public ActionResult<Response> DeleteProduct([FromQuery] int clientID, [FromRoute] int productID)
        {
            try
            {
                var result = _productService.DeleteProduct(clientID, productID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("products/{productID}")]
        [EndpointSummary("Edit a product")]
        [ProducesResponseType(typeof(ProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given product.")]
        public ActionResult<Response> EditProduct([FromBody] ProductEdit productEdit, [FromQuery] int clientID, [FromRoute] int productID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _productService.EditProduct(productEdit, clientID, productID);
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
