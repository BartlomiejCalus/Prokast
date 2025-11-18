using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Security.Cryptography;
using System.Linq;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.ClientModels;

namespace Prokast.Server.Services
{
    public class ClientService: IClientService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogInService _logInService;
        Random random = new Random();

        public ClientService(ProkastServerDbContext dbContext, IMapper mapper, ILogInService logInService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logInService = logInService;
        }

        public static string getHashed(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        #region RegisterClient
        public Response RegisterClient([FromBody] Registration registration) 
        { 
            
            var reg = _mapper.Map<Registration>(registration);
            if (reg == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Błędne dane rejestracji" };
            
            var role = _dbContext.Roles.FirstOrDefault(x => x.ID == 2);
            if (role == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Błąd przy przypisaniu roli" };

            var account = new Account
            {
                Login = registration.Login,
                Password = getHashed(registration.Password),
                RoleID = role.ID
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            var test = _dbContext.Accounts.FirstOrDefault(x => x.Login == account.Login);

            var client = new Client
            {
                FirstName = reg.FirstName,
                LastName = reg.LastName,
                BusinessName = reg.BusinessName,
                NIP = reg.NIP,
                Address = reg.Address,
                PhoneNumber = reg.PhoneNumber,
                PostalCode = reg.PostalCode,
                City = reg.City,
                Country = reg.Country
            };
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges(); 

            return new ClientRegisterResponse() { ID = random.Next(1, 100000), ClientID = test.ID, Registration = reg };
        }
        #endregion

    }
}
