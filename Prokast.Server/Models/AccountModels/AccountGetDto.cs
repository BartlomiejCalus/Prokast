using Prokast.Server.Entities;

namespace Prokast.Server.Models.AccountModels;

public class AccountGetDto (Account account)
{
    public int Id { get; set; } = account.ID;
    public string FirstName { get; set; } = account.FirstName!;
    public string LastName { get; set; } = account.LastName!;
    public int RoleId { get; set; } = (int)account.RoleID!;
    public int? WarehouseId { get; set; } = account.WarehouseID == null ? (int?)null : (int)account.WarehouseID;
}
