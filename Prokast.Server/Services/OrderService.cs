using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Enums;
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
        /// <summary>
        /// Funkcja ta tworzy zamówienie dla danego kupca, jeśli jest to pierwsze zamówienie tej osoby jest również rejestrowana jako nowy kupiec
        /// </summary>
        /// <param name="orderCreateDto"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Response CreateOrder(OrderCreateDto orderCreateDto, int clientID)
        {
            if (orderCreateDto == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var client = _dbContext.Clients.FirstOrDefault(x => x.ID == clientID);
            if (client == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego klienta!" };

            var buyer = _dbContext.Buyers.FirstOrDefault(x => x.Email == orderCreateDto.Email && x.PhoneNumber == orderCreateDto.PhoneNumber);
            if (buyer == null)
            {
                buyer = new Buyer()
                {
                    FirstName = orderCreateDto.FirstName,
                    LastName = orderCreateDto.LastName,
                    Email = orderCreateDto.Email,
                    PhoneNumber = orderCreateDto.PhoneNumber,
                    Address = orderCreateDto.Address,
                    HouseNumber = orderCreateDto.HouseNumber,
                    City = orderCreateDto.City,
                    PostalCode = orderCreateDto.PostalCode,
                    Country = orderCreateDto.Country,
                };
                _dbContext.Buyers.Add(buyer);
                _dbContext.SaveChanges();
            }

            if(orderCreateDto.IsBusiness is true)
            {
                var businessBuyer = _dbContext.Buyers.FirstOrDefault(x => x.Email == orderCreateDto.BusinessEmail && x.PhoneNumber == orderCreateDto.BusinessPhoneNumber);
                if (businessBuyer == null)
                {
                    businessBuyer = new Buyer()
                    {
                        FirstName = orderCreateDto.BusinessFirstName,
                        LastName = orderCreateDto.BusinessLastName,
                        Email = orderCreateDto.BusinessEmail,
                        PhoneNumber = orderCreateDto.BusinessPhoneNumber,
                        Address = orderCreateDto.BusinessAddress,
                        HouseNumber = orderCreateDto.BusinessHouseNumber,
                        City = orderCreateDto.BusinessCity,
                        PostalCode = orderCreateDto.BusinessPostalCode,
                        Country = orderCreateDto.BusinessCountry,
                        NIP = orderCreateDto.NIP
                        
                    };
                    _dbContext.Buyers.Add(businessBuyer);
                    _dbContext.SaveChanges();
                }
            }

            buyer = _dbContext.Buyers.FirstOrDefault(x => x.Email == orderCreateDto.Email && x.PhoneNumber == orderCreateDto.PhoneNumber);
            var business = _dbContext.Buyers.FirstOrDefault(x => x.Email == orderCreateDto.BusinessEmail && x.PhoneNumber == orderCreateDto.BusinessPhoneNumber);

            var order = new Order()
            {
                OrderID = orderCreateDto.OrderID,
                OrderDate = orderCreateDto.OrderDate,
                OrderStatus = OrderStatus.pending,
                TotalPrice = orderCreateDto.TotalPrice,
                TotalWeightKg = orderCreateDto.TotalWeightKg,
                PaymentMethod = orderCreateDto.PaymentMethod,
                PaymentStatus = PaymentStatus.pending,
                UpdateDate = orderCreateDto.UpdateDate,
                ClientID = clientID,
                IsBusiness = false,
                BuyerID = buyer.ID
            };

            if(business != null)
            {
                order.BusinessID = business.ID;
                order.IsBusiness = true;
            }

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();



            /*var createdOrder = _dbContext.Orders.OrderByDescending(x => x.ID).FirstOrDefault();
            if(createdOrder == null)
            {
                responseNull.errorMsg = "Błąd zamówienia!";
                return responseNull;
            }*/

            client.Orders.Add(order);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }
        #endregion

        #region Get
        public Response GetAllOrders(int clientID)
        {
            var orderList = _dbContext.Orders.Where(x => x.ClientID == clientID).ToList();
            if (orderList.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówień!" };

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

            return new OrderGetAllResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = returnList };
        }

        public Response GetOrder(int clientID, int orderID)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };

            var buyer = _dbContext.Buyers.FirstOrDefault(x => x.ID == order.BuyerID);

            var businessBuyer = _dbContext.Buyers.FirstOrDefault(x => x.ID == order.BusinessID);

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
                BuyerID = order.BuyerID,
                FirstName = buyer.FirstName,
                LastName = buyer.LastName,
                Email = buyer.Email,
                PhoneNumber = buyer.PhoneNumber,
                Address = buyer.Address,
                HouseNumber = buyer.HouseNumber,
                PostalCode = buyer.PostalCode,
                Country = buyer.Country,
                City = buyer.City,
            };
            
            if (businessBuyer!= null)
            {
                newOrder.BusinessFirstName = businessBuyer.FirstName;
                newOrder.BusinessLastName = businessBuyer.LastName;
                newOrder.BusinessEmail = businessBuyer.Email;
                newOrder.BusinessPhoneNumber = businessBuyer.PhoneNumber;
                newOrder.BusinessAddress = businessBuyer.Address;
                newOrder.BusinessHouseNumber = businessBuyer.HouseNumber;
                newOrder.BusinessPostalCode = businessBuyer.PostalCode;
                newOrder.BusinessCity = businessBuyer.City;
                newOrder.BusinessCountry = businessBuyer.Country;
                newOrder.NIP = businessBuyer.NIP;
            }

            return new OrderGetOneResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = newOrder };
        }

        public Response GetOrderByTrackingID(int clientID, string trackingID)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.TrackingID == trackingID);
            if (order == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };

            var buyer = _dbContext.Buyers.FirstOrDefault(x => x.ID == order.BuyerID);

            var businessBuyer = _dbContext.Buyers.FirstOrDefault(x => x.ID == order.BusinessID);

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
                BuyerID = order.BuyerID,
                FirstName = buyer.FirstName,
                LastName = buyer.LastName,
                Email = buyer.Email,
                PhoneNumber = buyer.PhoneNumber,
                Address = buyer.Address,
                HouseNumber = buyer.HouseNumber,
                PostalCode = buyer.PostalCode,
                Country = buyer.Country,
                City = buyer.City,
            };

            if (businessBuyer != null)
            {
                newOrder.BusinessFirstName = businessBuyer.FirstName;
                newOrder.BusinessLastName = businessBuyer.LastName;
                newOrder.BusinessEmail = businessBuyer.Email;
                newOrder.BusinessPhoneNumber = businessBuyer.PhoneNumber;
                newOrder.BusinessAddress = businessBuyer.Address;
                newOrder.BusinessHouseNumber = businessBuyer.HouseNumber;
                newOrder.BusinessPostalCode = businessBuyer.PostalCode;
                newOrder.BusinessCity = businessBuyer.City;
                newOrder.BusinessCountry = businessBuyer.Country;
                newOrder.NIP = businessBuyer.NIP;
            }

            return new OrderGetOneResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = newOrder };
        }
        #endregion

        public Response AddTrackingID(int clientID, int orderID, string trackingID)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };
            order.TrackingID = trackingID;
            _dbContext.SaveChanges();
            
            var orderStatus = OrderStatus.shipped;
            ChangeOrderStatus(clientID, orderID, orderStatus);

            return new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
        }
        /// <summary>
        /// Funkcja zmienia status zamówienia
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="orderID"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response ChangeOrderStatus(int clientID, int orderID, OrderStatus request)
        {

            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };

            order.OrderStatus = request;
            order.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();

            return new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
        }
        /// <summary>
        /// Funkcja zmienia status plątności zamówienia
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="orderID"></param>
        /// <param name="paymentStatus"></param>
        /// <returns></returns>
        public Response ChangePaymentStatus(int clientID, int orderID, PaymentStatus paymentStatus)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };

            order.PaymentStatus = paymentStatus;
            order.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();

            return new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
        }

        /// <summary>
        /// Funkcja edytuje zamówienie
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="orderID"></param>
        /// <param name="orderEditDto"></param>
        /// <returns></returns>
        public Response EditOrder(int clientID, int orderID, OrderEditDto orderEditDto)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.ID == orderID);
            if (order == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };

            order.OrderID = orderEditDto.OrderID;
            order.TotalPrice = orderEditDto.TotalPrice;
            order.TotalWeightKg = orderEditDto.TotalWeightKg;
            order.PaymentMethod = orderEditDto.PaymentMethod;
            order.ClientID = orderEditDto.ClientID;
            order.BuyerID = orderEditDto.BuyerID;
            order.BusinessID = orderEditDto.BusinessID;
            order.UpdateDate = DateTime.Now;
            _dbContext.SaveChanges();

            return new OrderEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = order };
        }

        public Response EditBuyer(int clientID, int buyerID, Buyer buyerDto)
        {
            var buyer = _dbContext.Buyers.FirstOrDefault(x => x.ID == buyerID);
            if (buyer == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak zamówienia!" };

            buyer.ID = buyerID;
            buyer.FirstName = buyerDto.FirstName;
            buyer.LastName = buyerDto.LastName;
            buyer.Email = buyerDto.Email;
            buyer.PhoneNumber = buyerDto.PhoneNumber;
            buyer.Address = buyerDto.Address;

            if(buyerDto.HouseNumber == null)
            {
                buyer.HouseNumber = null;
            }
            else
            {
                buyer.HouseNumber = buyerDto.HouseNumber;
            }

            buyer.PostalCode = buyerDto.PostalCode;
            buyer.Country = buyerDto.Country;
            buyer.City = buyerDto.City;
            buyer.NIP = buyerDto.NIP;
            _dbContext.SaveChanges();

            return new BuyerEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = buyer };
        }
    
    }
}
