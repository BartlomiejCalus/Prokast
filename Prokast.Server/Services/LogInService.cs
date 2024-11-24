using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using System.Text;
using System;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.AspNetCore.Mvc;



namespace Prokast.Server.Services
{


    public class LogInService :  ILogInService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        Random random = new Random();

        public LogInService(ProkastServerDbContext dbContext, IMapper mapper)
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

        #region GetAll
        public Response GetLogIns(int clientID)
        {
            var logins = _dbContext.AccountLogIn.ToList();
            var response = new Response() {ID = random.Next(1,100000),ClientID = clientID, Model = logins};
            return response;
        }
        #endregion

        #region LogIn
        public Response Log_In([FromBody] LoginRequest loginRequest)
        {

            var account = _dbContext.AccountLogIn.FirstOrDefault(x => x.Login == loginRequest.Login);
            if (account == null) { throw new Exception("Nie ma takiego konta!"); }
            var client = _dbContext.Clients.FirstOrDefault(x => x.AccountID == account.ID);
            if (client == null) { throw new Exception("Nie ma takiego klienta!"); }
            if (account.Password != getHashed(loginRequest.Password)) { throw new Exception("Niepoprawne hasło"); }
            if (client.Subscription is null || client.Subscription < DateTime.Now)
            {
                var responseFalse = new Response() { ID = random.Next(1, 100000), ClientID = client.ID, Model = new { isSubscribed = false } };
                return responseFalse;
            }

            var response = new Response() {ID = random.Next(1,100000), ClientID = client.ID, Model = new {isSubscribed = true } };
            return response;
        }
        #endregion
    }
}
