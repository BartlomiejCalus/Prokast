using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public Response CreateStoredProduct(int warehouseID, int clientID, StoredProductCreateDto storedProducts)
        {
            if (storedProducts == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID);
            if (warehouse == null)
            {
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu" };
            }

            var storedProduct = new StoredProduct
            {
                Quantity = storedProducts.Quantity,
                MinQuantity = storedProducts.MinQuantity,
                WarehouseID = warehouseID,
                ProductID = storedProducts.ProductID,
            };
            _dbContext.StoredProducts.Add(storedProduct);
            _dbContext.SaveChanges();

            return new Response() { ID = random.Next(1, 100000), ClientID = clientID };
        }
        #endregion

        #region Get
        public Response GetAllStoredProducts(int clientID, int warehouseID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if(warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
            
            var storedProductsDb = _dbContext.StoredProducts.Include(x => x.Product).Where(x => x.WarehouseID == warehouseID).ToList();

            var storedProductsList = new List<StoredProductGetDto>();

            foreach ( var storedProduct in storedProductsDb)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ID == storedProduct.Product.ID);
                if (product == null)
                    return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
                
                var storedProductToList = new StoredProductGetDto()
                {
                    ID = storedProduct.ID,
                    WarehouseID = storedProduct.WarehouseID,
                    ProductID = storedProduct.Product.ID,
                    Quantity = storedProduct.Quantity,
                    MinQuantity = storedProduct.MinQuantity,
                    LastUpdated = storedProduct.LastUpdated,
                    ProductName = product.Name,
                    Sku = product.SKU
                };
                storedProductsList.Add(storedProductToList);
            }

            return new StoredProductGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProductsList };
        }
        
        public Response GetStoredProductByID(int clientID, int warehouseID, int ID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if (warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
            
            var storedProductDb = _dbContext.StoredProducts.Include(x => x.Product).FirstOrDefault(x => x.ID == ID);
            if(storedProductDb == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak produktów!" };

            var storedProductsList = new List<StoredProductGetDto>();
            var product = _dbContext.Products.FirstOrDefault(x => x.ID == storedProductDb.Product.ID);
            if (product == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            var storedProductToList = new StoredProductGetDto()
            {
                ID = storedProductDb.ID,
                WarehouseID = storedProductDb.WarehouseID,
                ProductID = storedProductDb.Product.ID,
                Quantity = storedProductDb.Quantity,
                MinQuantity = storedProductDb.MinQuantity,
                LastUpdated = storedProductDb.LastUpdated,
                ProductName = product.Name
            };
            storedProductsList.Add(storedProductToList);

            return new StoredProductGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProductsList };
        }

        public Response GetStoredProductsBelowMinimum(int clientID, int warehouseID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if (warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };

            var storedProductsDb = _dbContext.StoredProducts.Include(x => x.Product).Where(x => x.Quantity < x.MinQuantity && x.WarehouseID == warehouseID).ToList();
            if (storedProductsDb.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak produktów!" };

            var storedProductsList = new List<StoredProductGetDto>();

            foreach (var storedProduct in storedProductsDb)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ID == storedProduct.Product.ID);
                if (product == null)
                    return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

                var storedProductToList = new StoredProductGetDto()
                {
                    ID = storedProduct.ID,
                    WarehouseID = storedProduct.WarehouseID,
                    ProductID = storedProduct.Product.ID,
                    Quantity = storedProduct.Quantity,
                    MinQuantity = storedProduct.MinQuantity,
                    LastUpdated = storedProduct.LastUpdated,
                    ProductName = product.Name
                };
                storedProductsList.Add(storedProductToList);
            }

            return new StoredProductGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProductsList };
        }

        public Response GetStoredProductsBySKU(int clientID,int warehouseID, string SKU)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if (warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };
            
            var product = _dbContext.Products.FirstOrDefault(x => x.ClientID == clientID && x.SKU == SKU);
            if (product == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            var storedProduct = _dbContext.StoredProducts.FirstOrDefault(x => x.Product.ID == product.ID && x.WarehouseID == warehouseID);
            if (storedProduct == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Produktu nie ma na magazynie!" };

            var storedProductList = new List<StoredProductGetDto>();

            var storedProductToList = new StoredProductGetDto()
            {
                ID = storedProduct.ID,
                WarehouseID = storedProduct.WarehouseID,
                ProductID = storedProduct.Product.ID,
                Quantity = storedProduct.Quantity,
                MinQuantity = storedProduct.MinQuantity,
                LastUpdated = storedProduct.LastUpdated,
                ProductName = product.Name
            };
            storedProductList.Add(storedProductToList);

            return new StoredProductGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProductList };
        }

        public Response GetStoredProductsMinimalData(int clientID, int warehouseID)
        {
            var warehouse = _dbContext.Warehouses.FirstOrDefault(x => x.ID == warehouseID && x.ClientID == clientID);
            if (warehouse == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego magazynu!" };

            var storedProducts = _dbContext.StoredProducts.Include(x => x.Product).Where(x => x.WarehouseID == warehouseID).ToList();
            if (storedProducts.Count == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak produktów!" };
            
            var storedProductsList = new List<StoredProductGetMinimal>();

            //var productList = _dbContext.Products.Where(x => storedProducts.Select(y => y.Product.ID).Contains(x.ID)).ToList();

            foreach (var product in storedProducts)
            {
                if (product == null)
                    return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

               /* var storedProduct = storedProducts.FirstOrDefault(x => x.Product.ID == product.ID);
                if (storedProduct == null)
                    return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };*/
                
                var storedProductToList = new StoredProductGetMinimal()
                {
                    ID = product.ID,
                    SKU = product.Product.SKU,
                    Name = product.Product.Name,
                    Quantity = product.Quantity,
                    LastUpdated = product.LastUpdated
                };
                storedProductsList.Add(storedProductToList);
            }

            return new StoredProductGetMinimalResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProductsList };


        }
        #endregion

        #region Edit
        public Response EditStoredProductQuantity(int clientID, int ID, int quantity)
        {
            var storedProduct = _dbContext.StoredProducts.FirstOrDefault(x => x.ID == ID);
            if (storedProduct == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            storedProduct.Quantity += quantity;
            storedProduct.LastUpdated = DateTime.Now;
            _dbContext.SaveChanges();

            return new StoredProductEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProduct };
        }

        public Response EditStoredProductMinQuantity(int clientID, int ID, int minQuantity)
        {
            var storedProduct = _dbContext.StoredProducts.FirstOrDefault(x => x.ID == ID);
            if (storedProduct == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            storedProduct.MinQuantity = minQuantity;
            storedProduct.LastUpdated = DateTime.Now;
            _dbContext.SaveChanges();

            return new StoredProductEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProduct };
        }

        public Response EditMultipleStoredProductMinQuantity(int clientID, List<EditMultipleStoredProductMinQuantityDto> listToEdit)
        {
            var storedProduct = _dbContext.StoredProducts.Where(x => listToEdit.Select(y => y.ID).Contains(x.ID)).ToList();
            if (storedProduct.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            foreach(var product in storedProduct)
            {
                var min = listToEdit.FirstOrDefault(x => x.ID == product.ID);
                product.MinQuantity = min.MinQuantity;
                product.LastUpdated = DateTime.Now;
            }
            _dbContext.SaveChanges();

            return new StoredProductEditMulipleResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = storedProduct };
        }

        public Response EditStoredProduct(int clientID, StoredProductCreateDto storedProductEdit)
        {
            var storedProduct = _dbContext.StoredProducts.Include(x =>x.Product).FirstOrDefault(x => x.ID == storedProductEdit.ProductID && x.Product.ClientID == clientID);
            if (storedProduct == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };
            storedProduct.Quantity = storedProductEdit.Quantity;
            storedProduct.MinQuantity = storedProductEdit.MinQuantity;
            storedProduct.LastUpdated = DateTime.Now;
            _dbContext.SaveChanges();
            return new Response() { ID = random.Next(1, 100000), ClientID = clientID};
        }
        #endregion

        #region Delete
        public Response DeleteStoredProduct(int clientID, int ID)
        {
            var storedProduct = _dbContext.StoredProducts.FirstOrDefault(x => x.ID == ID);
            if (storedProduct == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego produktu!" };

            _dbContext.StoredProducts.Remove(storedProduct);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Produkt został usumięty" };
        }
        #endregion
    }
}
