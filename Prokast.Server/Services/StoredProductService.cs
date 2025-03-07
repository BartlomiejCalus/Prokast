﻿using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.StoredProductResponseModels;
using Prokast.Server.Models.StoredProductModels;
using Prokast.Server.Services.Interfaces;
using System.Web.Http;

namespace Prokast.Server.Services
{
    public class StoredProductService: IStoredProductService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public StoredProductService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Create
        public Response CreateStoredProduct([FromBody] StoredProductCreateMultipleDto storedProducts,int warehouseID, int clientID)
        {
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (storedProducts == null)
            {
                return responseNull;
            }
            
            foreach (var product in storedProducts.StoredProducts)
            {
                var storedProduct = new StoredProduct
                {
                    WarehouseID = warehouseID,
                    ProductID = product.ProductID,
                    Quantity = product.Quantity,
                    MinQuantity = product.MinQuantity
                };
                _dbContext.StoredProducts.Add(storedProduct);
                _dbContext.SaveChanges();
            }

            var response = new Response() { ID = random.Next(1, 100000), ClientID = clientID };
            return response;
        }
        #endregion

        #region Get
        public Response GetAllStoredProducts(int clientID, int warehouseID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if(warehouse == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
                return responseNull;
            }
            var storedProductsDb = _dbContext.StoredProducts.Where(x => x.WarehouseID == warehouseID).ToList();
            if (storedProductsDb.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak produktów!" };
                return responseNull;
            }

            var storedProductsList = new List<StoredProductGetDto>();

            foreach ( var storedProduct in storedProductsDb)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ID == storedProduct.ProductID);
                if (product == null)
                {
                    var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                    return responseNull;
                }
                var storedProductToList = new StoredProductGetDto()
                {
                    ID = storedProduct.ID,
                    WarehouseID = storedProduct.WarehouseID,
                    ProductID = storedProduct.ProductID,
                    Quantity = storedProduct.Quantity,
                    MinQuantity = storedProduct.MinQuantity,
                    LastUpdated = storedProduct.LastUpdated,
                    ProductName = product.Name
                };
                storedProductsList.Add(storedProductToList);
            }

            var response = new StoredProductGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProductsList };
            return response;
        }
        
        public Response GetStoredProductByID(int clientID, int warehouseID, int ID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if (warehouse == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
                return responseNull;
            }
            var storedProductDb = _dbContext.StoredProducts.FirstOrDefault(x => x.ID == ID);
            if(storedProductDb == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak produktów!" };
                return responseNull;
            }

            var storedProductsList = new List<StoredProductGetDto>();
            var product = _dbContext.Products.FirstOrDefault(x => x.ID == storedProductDb.ProductID);
            if (product == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                return responseNull;
            }
            var storedProductToList = new StoredProductGetDto()
            {
                ID = storedProductDb.ID,
                WarehouseID = storedProductDb.WarehouseID,
                ProductID = storedProductDb.ProductID,
                Quantity = storedProductDb.Quantity,
                MinQuantity = storedProductDb.MinQuantity,
                LastUpdated = storedProductDb.LastUpdated,
                ProductName = product.Name
            };
            storedProductsList.Add(storedProductToList);

            var response = new StoredProductGetResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = storedProductsList};
            return response;
        }

        public Response GetStoredProductsBelowMinimum(int clientID, int warehouseID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if (warehouse == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
                return responseNull;
            }
            var storedProductsDb = _dbContext.StoredProducts.Where(x => x.Quantity < x.MinQuantity && x.WarehouseID == warehouseID).ToList();
            if (storedProductsDb.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak produktów!" };
                return responseNull;
            }

            var storedProductsList = new List<StoredProductGetDto>();

            foreach (var storedProduct in storedProductsDb)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ID == storedProduct.ProductID);
                if (product == null)
                {
                    var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                    return responseNull;
                }
                var storedProductToList = new StoredProductGetDto()
                {
                    ID = storedProduct.ID,
                    WarehouseID = storedProduct.WarehouseID,
                    ProductID = storedProduct.ProductID,
                    Quantity = storedProduct.Quantity,
                    MinQuantity = storedProduct.MinQuantity,
                    LastUpdated = storedProduct.LastUpdated,
                    ProductName = product.Name
                };
                storedProductsList.Add(storedProductToList);
            }

            var response = new StoredProductGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProductsList};
            return response;
        }
        #endregion

        public Response EditStoredProductQuantity(int clientID, int ID, int quantity)
        {
            var storedProduct = _dbContext.StoredProducts.FirstOrDefault(x => x.ID == ID);
            if (storedProduct == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                return responseNull;
            }

            storedProduct.Quantity += quantity;
            storedProduct.LastUpdated = DateTime.Now;
            _dbContext.SaveChanges();

            var response = new StoredProductEditResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = storedProduct };
            return response;
        }

        public Response EditStoredProductMinQuantity(int clientID, int ID, int minQuantity)
        {
            var storedProduct = _dbContext.StoredProducts.FirstOrDefault(x => x.ID == ID);
            if (storedProduct == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                return responseNull;
            }

            storedProduct.MinQuantity = minQuantity;
            _dbContext.SaveChanges();

            var response = new StoredProductEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProduct };
            return response;
        }

        public Response EditMultipleStoredProductMinQuantity(int clientID, List<EditMultipleStoredProductMinQuantityDto> listToEdit)
        {
            var storedProduct = _dbContext.StoredProducts.Where(x => listToEdit.Select(y => y.ID).Contains(x.ID)).ToList();
            if (storedProduct.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                return responseNull;
            }

            foreach(var product in storedProduct)
            {
                var min = listToEdit.FirstOrDefault(x => x.ID == product.ID);
                product.MinQuantity = min.MinQuantity;
            }
            _dbContext.SaveChanges();

            var response = new StoredProductEditMulipleResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProduct };
            return response;
        }

        #region Delete
        public Response DeleteStoredProduct(int clientID, int ID)
        {
            var storedProduct = _dbContext.StoredProducts.SingleOrDefault(x => x.ID == ID);
            if (storedProduct == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                return responseNull;
            }

            _dbContext.StoredProducts.Remove(storedProduct);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Produkt został usumięty" };
            return response;
        }
        #endregion
    }
}
