using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ProductModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.ProductResponseModels;
using Prokast.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        public ActionResult<Response> CreateProduct([FromBody] ProductCreateDto productCreateDto, [FromQuery] int clientID, [FromQuery] int regionID)
        {
            try
            {
                var result = _productService.CreateProduct(productCreateDto, clientID, regionID);
                if (result is ErrorResponse) return BadRequest(result);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpPut("products/{productID}")]
        [ProducesResponseType(typeof(ProductEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
