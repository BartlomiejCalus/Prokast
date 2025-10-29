﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.MainPageModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Services
{
    public class OthersService: IOthersService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public OthersService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public Response GetRegions()
        {
            var regions = _dbContext.Regions.ToList();
            if (regions.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Klient nie ma parametrów" };
                return responseNull;
            }

            var response = new RegionsResponse() { ID = random.Next(1, 100000), ClientID = -1, Model = regions };
            return response;
        }

        public Response GetMainPage(int clientID)
        {
            var warehouses = _dbContext.Warehouses.Where(x => x.ID == clientID).ToList();

            var warehouseIds = warehouses.Select(x => x.ID).ToList();

            var storedProducts = _dbContext.StoredProducts.Include(x => x.Product).Where(x => warehouseIds.Contains(x.WarehouseID)).ToList();
            /*if(warehouses.Count == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Klient nie ma magazynów" };
                return responseNull;
            }*/
            var warehouseCount = warehouses.Count;
            var storedProductsCount = storedProducts.Sum(x => x.Quantity);

            var warehouseConponentList = new List<WarehouseVolume>();
            foreach (var warehouse in warehouses)
            {
                var warehouseUnit = new WarehouseVolume()
                {
                    WarehouseName = warehouse.Name,
                    StoredProductsCount = storedProducts.Where(x => x.WarehouseID == warehouse.ID).Count(),
                    StoredProducts = storedProducts.Where(x => x.WarehouseID == warehouse.ID).Select(x => new StoredProductCount { StoredProductName = x.Product.Name, StoredProductQuantity = x.Quantity } ).ToList()
                };
                warehouseConponentList.Add(warehouseUnit);
            }

            var mostAbundantProduct = storedProducts.MaxBy(x => x.Quantity);
            var newestProduct = storedProducts.MaxBy(x => x.LastUpdated);

            var topProduct = new StoredProductCount()
            {
                StoredProductName = mostAbundantProduct != null ? mostAbundantProduct.Product.Name : "Brak produktów",
                StoredProductQuantity = mostAbundantProduct != null ? mostAbundantProduct.Quantity : 0
            };

            var lastDelivery = new StoredProductCount()
            {
                StoredProductName = newestProduct != null ? newestProduct.Product.Name : "Brak produktów",
                StoredProductQuantity = newestProduct != null ? newestProduct.Quantity : 0
            };

            var mainPageGet = new MainPageGet()
            {
                WarehouseCount = warehouseCount,
                StoredProductsCount = storedProductsCount,
                TopProduct = topProduct,
                LastDelivery = lastDelivery,
                WarehouseVolumeList = warehouseConponentList
            };

            return new MainPageGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = mainPageGet };

    
        }
    }
}
