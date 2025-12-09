using Prokast.Server.Entities;
using Prokast.Server.Models.StoredProductModels;

namespace Prokast.Server.Models.WarehouseModels;

public class WarehouseGetAllData(Warehouse warehouse, List<StoredProductGetDto> storedProducts)
{
    public int ID { get; set; } = warehouse.ID;
    public string Name { get; set; } = warehouse.Name;
    public string Address { get; set; } = warehouse.Address;
    public string PostalCode { get; set; } = warehouse.PostalCode;
    public string City { get; set; } = warehouse.City;
    public string Country { get; set; } = warehouse.Country;
    public string PhoneNumber { get; set; } = warehouse.PhoneNumber;
    public List<StoredProductGetDto> StoredProducts { get; set; } = storedProducts;
}
