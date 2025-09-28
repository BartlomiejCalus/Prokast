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
        Random random = new Random();

        public ClientService(ProkastServerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
            //var reg = _mapper.Map<Registration>(registration);
            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), errorMsg = "Błędne dane rejestracji" };
            if (registration == null)
            {
                return responseNull;
            }
            
            //var test = _dbContext.Accounts.FirstOrDefault(x => x.Login == account.Login);

            var client = new Client
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                BusinessName = registration.BusinessName,
                NIP = registration.NIP,
                Address = registration.Address,
                PhoneNumber = registration.PhoneNumber,
                PostalCode = registration.PostalCode,
                City = registration.City,
                Country = registration.Country
            };
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();

            var newClient = _dbContext.Clients.OrderByDescending(x => x.ID).FirstOrDefault();
            if(newClient == null)
            {
                responseNull.errorMsg = "Błąd klient nwm";
                return responseNull;
            }

            var account = new Account
            {
                Login = registration.Login,
                Password = getHashed(registration.Password),
                ClientID = newClient.ID
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            var response = new ClientRegisterResponse() { ID = random.Next(1, 100000), ClientID = account.ID, Registration = registration };
            return response;

        }
        #endregion

    }
}
