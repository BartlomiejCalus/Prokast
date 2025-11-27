using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Models.StoredProductModels;

namespace Prokast.Server.Services.Interfaces
{
    public interface IStoredProductService
    {
        Response CreateStoredProduct(int warehouseID, int clientID, StoredProductCreateDto storedProducts);
        Response GetAllStoredProducts(int clientID, int warehouseID);
        Response GetStoredProductByID(int clientID,int warehouseID, int ID);
        Response GetStoredProductsBelowMinimum(int clientID, int warehouseID);
        Response GetStoredProductsBySKU(int clientID, int warehouseID, string SKU);
        Response GetStoredProductsMinimalData(int clientID, int warehouseID);
        Response EditStoredProductQuantity(int clientID, int ID, int quantity);
        Response EditStoredProductMinQuantity(int clientID, int ID, int minQuantity);
        Response EditMultipleStoredProductMinQuantity(int clientID, List<EditMultipleStoredProductMinQuantityDto> listToEdit);
        Response DeleteStoredProduct(int clientID, int ID);
        Response EditStoredProduct(int clientID, StoredProductCreateDto storedProductEdit);
    }
}
