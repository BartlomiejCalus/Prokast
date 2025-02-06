﻿using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface ILogInService
    {
        Response GetLogIns(int clientID);
        Response Log_In([FromBody] LoginRequest loginRequest);
        Response CreateAccount(AccountCreateDto accountCreate, int clientID);
        Response EditAccount(AccountEditDto accountEdit, int clientID);
        Response EditPassword(AccountEditPasswordDto editPasswordDto, int clientID);
        Response DeleteAccount(int clientID, int ID);
    }
}