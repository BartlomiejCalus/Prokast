﻿using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.OrderModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.OrderResponseModels;
using Prokast.Server.Services.Interfaces;
using System.Text;

namespace Prokast.Server.Services
{
    public class OrderService: IOrderService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public OrderService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Create
        public Response CreateOrder(OrderCreateDto orderCreateDto, int clientID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (orderCreateDto == null)
            {
                return responseNull;
            }

            const string znaki = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder trackingID = new StringBuilder();
            
            for (int i = 0; i < 20; i++)
            {
                int index = random.Next(znaki.Length);
                trackingID.Append(znaki[index]);
            }

            var customer = _dbContext.Customers.FirstOrDefault(x => x.Email == orderCreateDto.Email && x.PhoneNumber == orderCreateDto.PhoneNumber);
            if (customer == null)
            {
                customer = new Customer()
                {
                    FirstName = orderCreateDto.FirstName,
                    LastName = orderCreateDto.LastName,
                    Email = orderCreateDto.Email,
                    PhoneNumber = orderCreateDto.PhoneNumber,
                };
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
            }
            customer = _dbContext.Customers.FirstOrDefault(x => x.Email == orderCreateDto.Email && x.PhoneNumber == orderCreateDto.PhoneNumber);

            var order = new Order()
            {
                ShopOrderID = orderCreateDto.ShopOrderID,
                OrderDate = orderCreateDto.OrderDate,
                TotalPrice = orderCreateDto.TotalPrice,
                TotalWeightKg = orderCreateDto.TotalWeightKg,
                PaymentMethod = orderCreateDto.PaymentMethod,
                CreationDate = orderCreateDto.CreationDate,
                UpdateDate = orderCreateDto.UpdateDate,
                TrackingID = trackingID.ToString(),
                ClientID = clientID,
                CustomerID = customer.ID,
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllOrders(int clientID)
        {
            var orderList = _dbContext.Orders.Where(x => x.ClientID == clientID).ToList();
            if (orderList.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówień!" };
                return responseNull;
            }

            var returnList = new List<OrderGetAllDto>();

            foreach (var order in orderList)
            {
                var orderMin = new OrderGetAllDto
                {
                    ID = order.ID,
                    ShopOrderID = order.ShopOrderID,
                    OrderStatus = order.OrderStatus,
                    PaymentStatus = order.PaymentStatus,
                };
                returnList.Add(orderMin);
            }

            var response = new OrderGetAllResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = returnList };
            return response;
        }

        public Response GetOrder(int clientID, int orderID)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };
                return responseNull;
            }

            var customer = _dbContext.Customers.FirstOrDefault(x => x.ID == order.CustomerID);

            var newOrder = new OrderGetOneDto()
            {
                ID = orderID,
                ShopOrderID=order.ShopOrderID,
                OrderDate = order.OrderDate,
                OrderStatus=order.OrderStatus,
                TotalPrice = order.TotalPrice,
                TotalWeightKg = order.TotalWeightKg,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus=order.PaymentStatus,
                CreationDate = order.CreationDate,
                UpdateDate = order.UpdateDate,
                TrackingID = order.TrackingID,
                ClientID = clientID,
                CustomerID = order.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
            };
            
            var response = new OrderGetOneResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = newOrder };
            return response;
        }

        public Response GetOrderByTrackingID(int clientID, string trackingID)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.TrackingID == trackingID);
            if (order == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };
                return responseNull;
            }

            var customer = _dbContext.Customers.FirstOrDefault(x => x.ID == order.CustomerID);

            var newOrder = new OrderGetOneDto()
            {
                ID = order.ID,
                ShopOrderID = order.ShopOrderID,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                TotalPrice = order.TotalPrice,
                TotalWeightKg = order.TotalWeightKg,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                CreationDate = order.CreationDate,
                UpdateDate = order.UpdateDate,
                TrackingID = order.TrackingID,
                ClientID = clientID,
                CustomerID = order.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
            };

            var response = new OrderGetOneResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = newOrder };
            return response;
        }
        #endregion

        public Response ChangeOrderStatus(int clientID, int orderID, string status)
        {
            var statuses = new List<string>()
            {
                "pending",
                "processing", 
                "shipped",
                "delivered",
                "cancelled",
                "returned"
            };

            if (!statuses.Contains(status))
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędny status!" };
                return responseNull;
            }

            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };
                return responseNull;
            }

            order.OrderStatus = status;
            _dbContext.SaveChanges();

            var response = new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
            return response;
        }

        public Response ChangePaymentStatus(int clientID, int orderID, string paymentStatus)
        {
            var statuses = new List<string>()
            {
                "pending",
                "paid",
                "failed",
                "refunded"
            };

            if (!statuses.Contains(paymentStatus))
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędny status!" };
                return responseNull;
            }

            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };
                return responseNull;
            }

            order.PaymentStatus = paymentStatus;
            _dbContext.SaveChanges();

            var response = new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
            return response;
        }
    
    }
}
