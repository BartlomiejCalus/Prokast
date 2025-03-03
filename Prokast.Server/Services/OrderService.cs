using AutoMapper;
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

            if(orderCreateDto.BusinessFirstName != null && orderCreateDto.BusinessLastName != null && orderCreateDto.BusinessEmail != null && orderCreateDto.BusinessPhoneNumber != null)
            {
                var businessCustomer = _dbContext.Customers.FirstOrDefault(x => x.Email == orderCreateDto.BusinessEmail && x.PhoneNumber == orderCreateDto.BusinessPhoneNumber);
                if (businessCustomer == null)
                {
                    businessCustomer = new Customer()
                    {
                        FirstName = orderCreateDto.BusinessFirstName,
                        LastName = orderCreateDto.BusinessLastName,
                        Email = orderCreateDto.BusinessEmail,
                        PhoneNumber = orderCreateDto.BusinessPhoneNumber,
                    };
                    _dbContext.Customers.Add(businessCustomer);
                    _dbContext.SaveChanges();
                }
            }

            customer = _dbContext.Customers.FirstOrDefault(x => x.Email == orderCreateDto.Email && x.PhoneNumber == orderCreateDto.PhoneNumber);
            var business = _dbContext.Customers.FirstOrDefault(x => x.Email == orderCreateDto.BusinessEmail && x.PhoneNumber == orderCreateDto.BusinessPhoneNumber);

            var order = new Order()
            {
                OrderID = orderCreateDto.OrderID,
                OrderDate = orderCreateDto.OrderDate,
                TotalPrice = orderCreateDto.TotalPrice,
                TotalWeightKg = orderCreateDto.TotalWeightKg,
                PaymentMethod = orderCreateDto.PaymentMethod,
                UpdateDate = orderCreateDto.UpdateDate,
                ClientID = clientID,
                CustomerID = customer.ID,
            };

            if(business != null)
            {
                order.BusinessID = business.ID;
            }

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
                    OrderID = order.OrderID,
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

            var businessCustomer = _dbContext.Customers.FirstOrDefault(x => x.ID == order.BusinessID);

            var newOrder = new OrderGetOneDto()
            {
                ID = orderID,
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                OrderStatus=order.OrderStatus,
                TotalPrice = order.TotalPrice,
                TotalWeightKg = order.TotalWeightKg,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus=order.PaymentStatus,
                UpdateDate = order.UpdateDate,
                TrackingID = order.TrackingID,
                ClientID = clientID,
                CustomerID = order.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
            };
            
            if (businessCustomer!= null)
            {
                newOrder.BusinessFirstName = businessCustomer.FirstName;
                newOrder.BusinessLastName = businessCustomer.LastName;
                newOrder.BusinessEmail = businessCustomer.Email;
                newOrder.BusinessPhoneNumber = businessCustomer.PhoneNumber;
            }

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

            var businessCustomer = _dbContext.Customers.FirstOrDefault(x => x.ID == order.BusinessID);

            var newOrder = new OrderGetOneDto()
            {
                ID = order.ID,
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                TotalPrice = order.TotalPrice,
                TotalWeightKg = order.TotalWeightKg,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                UpdateDate = order.UpdateDate,
                TrackingID = order.TrackingID,
                ClientID = clientID,
                CustomerID = order.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
            };

            if (businessCustomer != null)
            {
                newOrder.BusinessFirstName = businessCustomer.FirstName;
                newOrder.BusinessLastName = businessCustomer.LastName;
                newOrder.BusinessEmail = businessCustomer.Email;
                newOrder.BusinessPhoneNumber = businessCustomer.PhoneNumber;
            }

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

            
            const string znaki = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder trackingID = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                int index = random.Next(znaki.Length);
                trackingID.Append(znaki[index]);
            }

            order.OrderStatus = status;
            order.UpdateDate = DateTime.Now;

            if (status == "shipped")
            {
                order.TrackingID = trackingID.ToString();
            }

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
            order.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();

            var response = new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
            return response;
        }

        public Response EditOrder(int clientID, int orderID, OrderEditDto orderEditDto)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };
                return responseNull;
            }

            order.OrderID = orderEditDto.OrderID;
            order.TotalPrice = orderEditDto.TotalPrice;
            order.TotalWeightKg = orderEditDto.TotalWeightKg;
            order.PaymentMethod = orderEditDto.PaymentMethod;
            order.ClientID = orderEditDto.ClientID;
            order.CustomerID = orderEditDto.CustomerID;
            order.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();

            var response = new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
            return response;
        }

        public Response EditCustomer(int clientID, int customerID, Customer customerDto)
        {
            var customer = _dbContext.Customers.FirstOrDefault(x => x.ID == customerID);
            if (customer == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };
                return responseNull;
            }

            customer.ID = customerID;
            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.Email = customerDto.Email;
            customer.PhoneNumber = customerDto.PhoneNumber;
            _dbContext.SaveChanges();

            var response = new CustomerEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = customer };
            return response;
        }
    
    }
}
