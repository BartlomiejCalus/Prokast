using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.WarehouseResponseModels;
using Prokast.Server.Models.WarehouseModels;
using Prokast.Server.Services.Interfaces;
using System.Web.Http;

namespace Prokast.Server.Services
{
    public class WarehouseService: IWarehouseService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public WarehouseService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Create
        public Response CreateWarehouse([FromBody] WarehouseCreateDto warehouseCreateDto, int clientID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (warehouseCreateDto == null)
            {
                return responseNull;
            }

            var client = _dbContext.Clients.FirstOrDefault(x => x.ID == clientID);
            if (client == null)
            {
                responseNull.errorMsg = "Nie ma takiego klienta!";
                return responseNull;
            }

            var warehouse = new Warehouse
            {
                Name = warehouseCreateDto.Name,
                Address = warehouseCreateDto.Address,
                PostalCode = warehouseCreateDto.PostalCode,
                City = warehouseCreateDto.City,
                Country = warehouseCreateDto.Country,
                PhoneNumber = warehouseCreateDto.PhoneNumber,
                ClientID = clientID,
            };

            _dbContext.Warehouses.Add(warehouse);
            _dbContext.SaveChanges();

            var createdWarehouse = _dbContext.Warehouses.OrderByDescending(x => x.ID).FirstOrDefault();
            if (createdWarehouse == null)
            {
                responseNull.errorMsg = "Błąd magazynu!";
                return responseNull;        
            }

            client.Warehouses.Add(createdWarehouse);
            _dbContext.SaveChanges();

            var response = new Response() { ID = random.Next(1,100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllWarehouses(int clientID)
        {
            var warehouseList = _dbContext.Warehouses.Where(x =>  x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };
                return responseNull;
            }
            var response = new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
            return response;
        }

        public Response GetWarehouseById(int clientID, int ID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == ID && x.ClientID == clientID);
            if (warehouse == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };
                return responseNull;
            }
            var response = new WarehouseGetOneResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = warehouse};
            return response;
        }
        public Response GetWarehousesByName(int clientID, string name)
        {
            var warehouseList = _dbContext.Warehouses.Where(x => x.Name.Contains(name) && x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };
                return responseNull;
            }

            var response = new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
            return response;
        }

        public Response GetWarehousesByCity(int clientID, string city)
        {
            var warehouseList = _dbContext.Warehouses.Where(x => x.Name.Contains(city) && x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };
                return responseNull;
            }

            var response = new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
            return response;
        }

        public Response GetWarehouseByCountry(int clientID, string country)
        {
            var warehouseList = _dbContext.Warehouses.Where(x => x.Name.Contains(country) && x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };
                return responseNull;
            }

            var response = new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
            return response;
        }

        public Response GetWarehousesMinimalData(int clientID)
        {
            var warehouses = _dbContext.Warehouses.Where(x => x.ClientID == clientID).ToList();
            if (warehouses.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };
                return responseNull;
            }

            var warehousesList = new List<WarehouseGetMinimal>();

            foreach(var warehouse in warehouses)
            {
                var warehouseToList = new WarehouseGetMinimal()
                {
                    ID = warehouse.ID,
                    Name = warehouse.Name
                };
                warehousesList.Add(warehouseToList);
            }

            var response = new WarehouseGetMinimalResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = warehousesList};
            return response;
        }
        #endregion

        #region Edit

        public Response EditWarehouse(int clientID, int ID, WarehouseCreateDto warehouseCreateDto)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);
            if (warehouse == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
                return responseNull;
            }

            warehouse.Name = warehouseCreateDto.Name;
            warehouse.Address = warehouseCreateDto.Address;
            warehouse.PostalCode = warehouseCreateDto.PostalCode;
            warehouse.City = warehouseCreateDto.City;
            warehouse.Country = warehouseCreateDto.Country;
            warehouse.PhoneNumber = warehouseCreateDto.PhoneNumber;
            _dbContext.SaveChanges();

            var response = new WarehouseEditResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = warehouse };
            return response;
        }
        #endregion

        #region Delete
        public Response DeleteWarehouse(int clientID, int ID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);
            if (warehouse == null)
            {
                return responseNull;
            }

            var client = _dbContext.Clients.FirstOrDefault(x => x.ID == clientID);
            if (client == null)
            {
                responseNull.errorMsg = "Nie ma takiego klienta!";
                return responseNull;
            }

            var storedProducts = _dbContext.StoredProducts.Where(x => x.WarehouseID == warehouse.ID).ToList();
            var workers = _dbContext.Accounts.Where(x => x.WarehouseID == warehouse.ID).ToList();
            foreach ( var storedProduct in storedProducts)
            {
                _dbContext.StoredProducts.Remove(storedProduct);
                _dbContext.SaveChanges();
            }

            foreach ( var worker in workers)
            {
                worker.WarehouseID = null;
                _dbContext.SaveChanges();
            }

            client.Warehouses.Remove(warehouse);

            _dbContext.Warehouses.Remove(warehouse);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Magazyn został usumięty" };
            return response;
        }
        #endregion
    }
}
