using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            if (warehouseCreateDto == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var client = _dbContext.Clients.Include(x => x.Warehouses).FirstOrDefault(x => x.ID == clientID);
            if (client == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego klienta!" };

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

            client.Warehouses.Add(warehouse);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }
        #endregion

        #region Get
        public Response GetAllWarehouses(int clientID)
        {
            var warehouseList = _dbContext.Warehouses.Where(x =>  x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };

            return new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
        }

        public Response GetWarehouseById(int clientID, int ID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == ID && x.ClientID == clientID);
            if (warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };

            return new WarehouseGetOneResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouse };
        }
        public Response GetWarehousesByName(int clientID, string name)
        {
            var warehouseList = _dbContext.Warehouses.Where(x => x.Name.Contains(name) && x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };

            return new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
        }

        public Response GetWarehousesByCity(int clientID, string city)
        {
            var warehouseList = _dbContext.Warehouses.Where(x => x.City.Contains(city) && x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };

            return new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
        }

        public Response GetWarehouseByCountry(int clientID, string country)
        {
            var warehouseList = _dbContext.Warehouses.Where(x => x.Country.Contains(country) && x.ClientID == clientID).ToList();
            if (warehouseList.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };

            return new WarehouseGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouseList };
        }

        public Response GetWarehousesMinimalData(int clientID)
        {
            var warehouses = _dbContext.Warehouses.Where(x => x.ClientID == clientID).ToList();
            if (warehouses.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak magazynów!" };

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

            return new WarehouseGetMinimalResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehousesList };
        }
        #endregion

        #region Edit

        public Response EditWarehouse(int clientID, int ID, WarehouseCreateDto warehouseCreateDto)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);
            if (warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };

            warehouse.Name = warehouseCreateDto.Name;
            warehouse.Address = warehouseCreateDto.Address;
            warehouse.PostalCode = warehouseCreateDto.PostalCode;
            warehouse.City = warehouseCreateDto.City;
            warehouse.Country = warehouseCreateDto.Country;
            warehouse.PhoneNumber = warehouseCreateDto.PhoneNumber;
            _dbContext.SaveChanges();

            return new WarehouseEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = warehouse };
        }
        #endregion

        #region Delete
        public Response DeleteWarehouse(int clientID, int ID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ClientID == clientID && x.ID == ID);
            if (warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };

            var client = _dbContext.Clients.FirstOrDefault(x => x.ID == clientID);
            if (client == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego klienta!" };

            //var storedProducts = _dbContext.StoredProducts.Where(x => x.WarehouseID == warehouse.ID).ToList();
            //var workers = _dbContext.Accounts.Where(x => x.WarehouseID == warehouse.ID).ToList();
            /*foreach ( var storedProduct in storedProducts)
            {
                _dbContext.StoredProducts.Remove(storedProduct);
                _dbContext.SaveChanges();
            }*/

            /*foreach ( var worker in workers)
            {
                worker.WarehouseID = null;
                _dbContext.SaveChanges();
            }*/

            //client.Warehouses.Remove(warehouse);

            _dbContext.Warehouses.Remove(warehouse);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Magazyn został usumięty" };
        }
        #endregion
    }
}
