using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Models.ProductModels;
using Prokast.Server.Models.ResponseModels;

namespace Prokast.Server.Services.Interfaces
{
    public interface IProductService
    {
        Response CreateProduct(ProductCreateDto productCreateDto, int clientID, int regionID);
        Response GetOneProduct(int clientID, int productID);
        Response GetProducts(int clientID, ProductFilter filter, int pageNumber, int itemsNumber);
        Response EditProduct(ProductEdit productEdit, int clientID, int productID);
        Response DeleteProduct(int clientID, int productID);

    }
}
