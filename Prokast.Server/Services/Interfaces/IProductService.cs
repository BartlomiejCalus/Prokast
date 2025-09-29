using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Models.ProductModels;
using Prokast.Server.Models.ResponseModels;

namespace Prokast.Server.Services.Interfaces
{
    public interface IProductService
    {
        Response CreateProduct(ProductCreateDto productCreateDto, int clientID, int regionID);
        Response GetProducts(int clientID, string name, string sku);
        Response EditProduct(ProductEdit productEdit, int clientID, int productID);

    }
}
