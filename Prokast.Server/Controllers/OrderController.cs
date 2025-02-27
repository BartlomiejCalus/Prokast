﻿using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models;
using Prokast.Server.Services;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.OrderModels;
using Prokast.Server.Models.ResponseModels.WarehouseResponseModels;
using Prokast.Server.Models.ResponseModels.OrderResponseModels;

namespace Prokast.Server.Controllers
{
    [Route("api/orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(OrderGetAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(OrderGetAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(OrderGetAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [HttpPut("{orderID}/status")]
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> ChangeOrderStatus([FromQuery] int clientID, [FromRoute] int orderID,[FromBody] string status)
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
        [ProducesResponseType(typeof(OrderEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> ChangePaymentStatus([FromQuery] int clientID, [FromRoute] int orderID, [FromBody] string paymentStatus)
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
        #endregion
    }
}
