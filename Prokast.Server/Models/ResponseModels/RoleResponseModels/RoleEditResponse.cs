namespace Prokast.Server.Models.ResponseModels.RoleResponseModels;

public class RoleEditResponse : Response
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? Role { get; set; }
}