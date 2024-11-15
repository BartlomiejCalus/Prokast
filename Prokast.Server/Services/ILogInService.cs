﻿using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;

namespace Prokast.Server.Services
{
    public interface ILogInService
    {
        Response GetLogIns(int clientID);
        Response Log_In([FromBody] LoginRequest loginRequest);
    }
}