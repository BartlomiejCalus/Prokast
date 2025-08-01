﻿using Prokast.Server.Entities;
using Prokast.Server.Enums;
using Prokast.Server.Models;
using Prokast.Server.Models.OrderModels;

namespace Prokast.Server.Services.Interfaces
{
    public interface IOrderService
    {
        Response CreateOrder(OrderCreateDto orderCreateDto, int clientID);
        Response GetAllOrders(int clientID);
        Response GetOrder(int clientID, int orderID);
        Response GetOrderByTrackingID(int clientID, string trackingID);
        Response AddTrackingID(int clientID, int orderID, string trackingID);
        Response ChangeOrderStatus(int clientID, int orderID, OrderStatus status);
        Response ChangePaymentStatus(int clientID, int orderID, PaymentStatus paymentStatus);
        Response EditOrder(int clientID, int orderID, OrderEditDto orderEditDto);
        Response EditBuyer(int clientID, int buyerID, Buyer buyerDto);
    }
}
