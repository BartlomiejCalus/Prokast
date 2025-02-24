﻿using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IStoredProductService
    {
        Response CreateStoredProduct([FromBody] StoredProductCreateMultipleDto storedProducts, int warehouseID, int clientID);
        Response GetAllStoredProducts(int clientID, int warehouseID);
        Response GetStoredProductByID(int clientID,int warehouseID, int ID);
        Response GetStoredProductsBelowMinimum(int clientID, int warehouseID);
        Response EditStoredProductQuantity(int clientID, int ID, int quantity);
        Response EditStoredProductMinQuantity(int clientID, int ID, int minQuantity);
        Response EditMultipleStoredProductMinQuantity(int clientID, List<EditMultipleStoredProductMinQuantityDto> listToEdit);
        Response DeleteStoredProduct(int clientID, int ID);
    }
}
