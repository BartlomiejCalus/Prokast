using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ClientModels;
using Prokast.Server.Models.ProductModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.ProductResponseModels;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using System.Security.Claims;

namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateProduct([FromBody] ProductCreateDto productCreateDto, [FromQuery] int regionID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _productService.CreateProduct(productCreateDto, clientIdFromToken, regionID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpGet("products/{productID}")]
        [ProducesResponseType(typeof(ProductsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetOneProduct([FromRoute] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _productService.GetOneProduct(clientIdFromToken, productID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("products/{productID}")]
        [ProducesResponseType(typeof(ProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditProduct([FromBody] ProductEdit productEdit, [FromRoute] int productID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _productService.EditProduct(productEdit, clientIdFromToken, productID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        [EndpointSummary("Delete an additional description")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given product and all of its components.")]
        public ActionResult<Response> DeleteProduct([FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _productService.DeleteProduct(clientIdFromToken, ID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("productsListFiltered")]
        [ProducesResponseType(typeof(ProductGetMinResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> Getproducts( [FromBody] ProductFilter filter, [FromQuery] int pageNumber, [FromQuery] int itemsNumber)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var products = _productService.GetProducts(clientIdFromToken, filter, pageNumber, itemsNumber);
                if (products is ErrorResponse) return BadRequest(products);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
