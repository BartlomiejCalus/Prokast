using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using System.Text;
using System;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Models.ResponseModels.AccountResponseModels;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.AccountModels;



namespace Prokast.Server.Services
{


    public class LogInService :  ILogInService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMailingService _mailingService;
        Random random = new Random();

        public LogInService(ProkastServerDbContext dbContext, IMapper mapper, IMailingService mailingService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mailingService = mailingService;
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
            var logins = _dbContext.Accounts.ToList();
            if (logins.Count() == 0)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów" };
                return responseNull;
            }
            var response = new LogInGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = logins };
            return response;

        }
        #endregion

        #region LogIn
        public Response Log_In([FromBody] LoginRequest loginRequest)
        {

            var account = _dbContext.Accounts.FirstOrDefault(x => x.Login == loginRequest.Login);
            if (account == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Nie ma takiego konta" };
                return responseNull;
            }

            var client = _dbContext.Clients.FirstOrDefault(x => x.AccountID == account.ID);
            if (client == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Błędny login" };
                return responseNull;
            }
                

            if (account.Password != getHashed(loginRequest.Password))
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Błędne hasło" };
                return responseNull;
            }

            if (client.Subscription is null || client.Subscription < DateTime.Now)
            {
                var responseFalse = new LogInLoginResponse() { ID = random.Next(1, 100000), ClientID = client.ID, IsSubscribed = false };
                return responseFalse;
            }

            var response = new LogInLoginResponse() {ID = random.Next(1,100000), ClientID = client.ID, IsSubscribed = true };
            return response;
        }
        #endregion

        #region create
        /// <summary>
        /// Funkcja pozwala na stworzzenie konta użytkownika i wysyła e-mail z danymi logowania na konto mailowe podane podczas kreacji
        /// </summary>
        /// <param name="accountCreate"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Response CreateAccount(AccountCreateDto accountCreate, int clientID)
        {
            const string litery = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            if (accountCreate == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
                return responseNull;
            }

            string login = new string(accountCreate.FirstName.Take(3).Concat(accountCreate.LastName.Take(2)).
                Concat(random.Next(1,100000).ToString()).ToArray());
            StringBuilder password = new StringBuilder();

            foreach(char znak in login)
            {
                int index = random.Next(litery.Length);
                password.Append(litery[index]);
            }

            Console.WriteLine(login);
            Console.WriteLine(password.ToString());

            var newAccount = new Account
            {
                Login = login,
                Password = getHashed(password.ToString()),
                WarehouseID = accountCreate.WarehouseID,
                Role = accountCreate.Role,
                FirstName = accountCreate.FirstName,
                LastName = accountCreate.LastName,
                ClientID = clientID
            };

            _dbContext.Accounts.Add(newAccount);
            _dbContext.SaveChanges();

            var creds = new AccountCredentials()
            {
                Login = login,
                Password = getHashed(password.ToString()),
            };

            var message = new EmailMessage
            {
                To = [accountCreate.Email],
                Subject = "Dane Logowania",
                Body = $"Login: {login}\n Hasło: {password}"
            };

            _mailingService.SendEmail(message);

            var response = new AccountCredentialsResponse() {ID =  random.Next(1,100000), ClientID = clientID, Model = creds};
            return response;
        }
        #endregion

        #region Edit
        public Response EditAccount(AccountEditDto accountEdit, int clientID)
        {
            
            var account = _dbContext.Accounts.FirstOrDefault(x => x.ID == clientID && x.Login == accountEdit.Login);
            if (account == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
                return responseNull;
            }
            account.WarehouseID = accountEdit.WarehouseID;
            account.Role = accountEdit.Role;
            _dbContext.SaveChanges();

            var response = new AccountEditResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = accountEdit };
            return response;
        }

        public Response EditPassword(AccountEditPasswordDto editPasswordDto, int clientID)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.ID == clientID && x.Login == editPasswordDto.Login);
            if (account == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
                return responseNull;
            }

            account.Password = getHashed(editPasswordDto.Password);
            _dbContext.SaveChanges();

            var response = new AccountEditPasswordResponse() { ID = random.Next(1,100000), ClientID = clientID, Model = editPasswordDto };
            return response;
        }
        #endregion

        #region Delete
        public Response DeleteAccount(int clientID, int ID)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.ID == ID);
            if (account == null)
            {
                var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
                return responseNull;
            }

            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();

            var response = new DeleteResponse() { ID = random.Next(1,100000), ClientID = clientID, deleteMsg = "Konto zostało usunięte" };
            return response;
        }
        #endregion
    }
}
