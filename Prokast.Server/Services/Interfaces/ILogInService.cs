using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.AccountModels;
using Prokast.Server.Models.JWT;

namespace Prokast.Server.Services.Interfaces
{
    public interface ILogInService
    {
        Response GetLogIns(int clientID);
        string Log_In(LoginRequest loginRequest);
        Account CreateAccount(AccountCreateDto accountCreate, int clientID);
        Response EditAccount(AccountEditDto accountEdit, int clientID);
        Response EditPassword(AccountEditPasswordDto editPasswordDto, int clientID);
        Response DeleteAccount(int clientID, int ID);

    }
}