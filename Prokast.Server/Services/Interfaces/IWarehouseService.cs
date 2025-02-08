﻿using Prokast.Server.Models;
using System.Web.Http;

namespace Prokast.Server.Services.Interfaces
{
    public interface IWarehouseService
    {
        Response CreateWarehouse([FromBody] WarehouseCreateDto warehouseCreateDto, int clientID);
        Response GetAllWarehouses(int clientID);
        Response GetWarehouseById(int clientID, int ID);
        Response GetWarehousesByName(int clientID, string name);
        Response GetWarehousesByCity(int clientID, string city);
        Response GetWarehouseByCountry(int clientID, string country);
        Response EditWarehouse(int clientID, int ID, WarehouseCreateDto warehouseCreateDto);
        Response DeleteWarehouse(int clientID, int ID);
    }
}
