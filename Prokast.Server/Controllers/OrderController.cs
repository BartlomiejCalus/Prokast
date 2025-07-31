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

namespace Prokast.Server.Controllers
{
    [Route("api/orders")]
    [Tags("Orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #region Create
        [HttpPost]
        [EndpointSummary("Create order")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint registers an order and registers a buyer if it's his first time buying products.")]
        public ActionResult<Response> CreateOrder([FromBody] OrderCreateDto orderCreateDto, [FromQuery] int clientID)
        {
            try
            {
                var result = _orderService.CreateOrder(orderCreateDto,clientID);
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
        [EndpointSummary("Get all orders")]
        [ProducesResponseType(typeof(OrderGetAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a minimalistic list of all orders made for a given client.")]
        public ActionResult<Response> GetAllOrders([FromQuery] int clientID)
        {
            try
            {
                var result = _orderService.GetAllOrders(clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderID}")]
        [EndpointSummary("Get an order")]
        [ProducesResponseType(typeof(OrderGetOneResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific order.")]
        public ActionResult<Response> GetOrder([FromQuery] int clientID, [FromRoute] int orderID)
        {
            try
            {
                var result = _orderService.GetOrder(clientID, orderID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("trackingID/{trackingID}")]
        [EndpointSummary("Get order by trackingID")]
        [ProducesResponseType(typeof(OrderGetAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns a specific order with a given trackingID.")]
        public ActionResult<Response> GetOrderByTrackingID([FromQuery] int clientID, [FromRoute] string trackingID)
        {
            try
            {
                var result = _orderService.GetOrderByTrackingID(clientID, trackingID);
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
        [EndpointSummary("Add trackingID")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint adds a trackingID for a given order.")]
        public ActionResult<Response> AddTrackingID([FromQuery] int clientID, [FromRoute] int orderID, [FromQuery] string trackingID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.AddTrackingID(clientID, orderID, trackingID);
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
        [EndpointSummary("Change order status")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint changes the status of a given order.")]
        public ActionResult<Response> ChangeOrderStatus([FromQuery] int clientID, [FromRoute] int orderID,[FromQuery] OrderStatus status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result =_orderService.ChangeOrderStatus(clientID, orderID, status);
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
        [EndpointSummary("Change payment status")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint changes the payment status of a given order.")]
        public ActionResult<Response> ChangePaymentStatus([FromQuery] int clientID, [FromRoute] int orderID, [FromQuery] PaymentStatus paymentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.ChangePaymentStatus(clientID, orderID, paymentStatus);
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
        [EndpointSummary("Edit an order")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given order.")]
        public ActionResult<Response> EditOrder([FromQuery] int clientID, [FromRoute] int orderID, [FromBody] OrderEditDto orderEditDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.EditOrder(clientID, orderID, orderEditDto);
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
        [EndpointSummary("Edit the buyer")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given buyer.")]
        public ActionResult<Response> EditBuyer([FromQuery] int clientID, [FromRoute] int buyerID, [FromBody] Buyer buyer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _orderService.EditBuyer(clientID, buyerID, buyer);
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
