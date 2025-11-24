using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Prokast.Server.Models.AccountModels;
using Prokast.Server.Models.JWT;
using Prokast.Server.Models.ResponseModels.RoleResponseModels;
using Prokast.Server.Models.ResponseModels;
using System;

namespace Prokast.Server.Services.Interfaces
{
    public interface ILogInService
    {
        Response GetLogIns(int clientID);
        Response Log_In(LoginRequest loginRequest);
        Response CreateAccount(AccountCreateDto accountCreate, int clientID, int roleId);
        Response EditAccount(AccountEditDto accountEdit, int clientID);
        Response EditPassword(AccountEditPasswordDto editPasswordDto, int clientID);
        Response DeleteAccount(int clientID, int ID);
        Response GetAllRoles(int clientID);
        Response GetRole(int clientID, int roleID);
        Response EditRole(int clientID, int accountID, int newRoleID, int roleID);
    }
}