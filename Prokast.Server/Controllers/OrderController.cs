using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.OrderModels;
using Prokast.Server.Models.ResponseModels.WarehouseResponseModels;
using Prokast.Server.Models.ResponseModels.OrderResponseModels;
using Prokast.Server.Entities;
using Prokast.Server.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Prokast.Server.Controllers
{
    [Authorize] 
    [Route("api/orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _orderService.CreateOrder(orderCreateDto, clientIdFromToken);
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
        [ProducesResponseType(typeof(OrderGetAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllOrders()
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _orderService.GetAllOrders(clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderID}")]
        [ProducesResponseType(typeof(OrderGetOneResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetOrder([FromRoute] int orderID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _orderService.GetOrder(clientIdFromToken, orderID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("trackingID/{trackingID}")]
        [ProducesResponseType(typeof(OrderGetAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetOrderByTrackingID([FromRoute] string trackingID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _orderService.GetOrderByTrackingID(clientIdFromToken, trackingID);
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
        [HttpPut("trackingID{orderID}")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> AddTrackingID([FromRoute] int orderID, [FromQuery] string trackingID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.AddTrackingID(clientIdFromToken, orderID, trackingID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{orderID}/status")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> ChangeOrderStatus([FromRoute] int orderID,[FromQuery] OrderStatus status)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result =_orderService.ChangeOrderStatus(clientIdFromToken, orderID, status);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{orderID}/payment")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> ChangePaymentStatus([FromRoute] int orderID, [FromQuery] PaymentStatus paymentStatus)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.ChangePaymentStatus(clientIdFromToken, orderID, paymentStatus);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit/order/{orderID}")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditOrder([FromRoute] int orderID, [FromBody] OrderEditDto orderEditDto)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.EditOrder(clientIdFromToken, orderID, orderEditDto);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("edit/buyer/{buyerID}")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditBuyer([FromRoute] int buyerID, [FromBody] Buyer buyer)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.EditBuyer(clientIdFromToken, buyerID, buyer);
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
