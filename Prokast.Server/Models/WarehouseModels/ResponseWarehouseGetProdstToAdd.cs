namespace Prokast.Server.Models.WarehouseModels;

public class ResponseWarehouseGetProdstToAdd : Response
{
    public List<WarehouseGetProdstToAddDto> Model { get; set; }
}

public class WarehouseGetProdstToAddDto
{
    public int ID { get; set; }
    public string Sku { get; set; }
}