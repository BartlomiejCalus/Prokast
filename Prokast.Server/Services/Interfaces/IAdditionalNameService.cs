﻿using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Entities;
using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IAdditionalNameService
    {
        Response CreateAdditionalName([FromBody] AdditionalNameDto additionalNameDto, int clientID);
        Response GetAllNames(int clientID);
        Response GetNamesByID(int ID, int clientID);
        Response GetNamesByIDNames(int ID, string Title, int clientID);
        Response GetNamesByIDRegion(int ID, int Region, int clientID);
        Response EditAdditionalName(int clientID, int ID, AdditionalNameDto data);
        Response DeleteAdditionalName(int clientID, int ID);
    }
}