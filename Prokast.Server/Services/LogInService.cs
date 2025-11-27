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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Prokast.Server.Models.JWT;
using Prokast.Server.Models.ClientModels;
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Models.ResponseModels.RoleResponseModels;




namespace Prokast.Server.Services
{


    public class LogInService :  ILogInService
    {
        private readonly ProkastServerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMailingService _mailingService;
        Random random = new Random();
        private readonly IConfiguration _configuration;

        public LogInService(ProkastServerDbContext dbContext, IMapper mapper, IMailingService mailingService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mailingService = mailingService;
            _configuration = configuration;
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
            var logins = _dbContext.Accounts.Where(x => x.ClientID == clientID).ToList();
            if (logins.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie ma parametrów" };

            return new LogInGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = [.. logins.Select(x => new AccountGetDto(x))] };

        }
        #endregion

        #region LogIn
        public Response Log_In([FromBody] LoginRequest loginRequest)
        {

            var account = _dbContext.Accounts.FirstOrDefault(x => x.Login == loginRequest.Login);
            if (account == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Nie ma takiego konta" };

            var client = _dbContext.Clients.FirstOrDefault(x => x.Accounts.Any(y => y.ID == account.ID));
            if (client == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Błędny login" };
                

            if (account.Password != getHashed(loginRequest.Password))
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = -1, errorMsg = "Błędne hasło" };

            /*if (client.Subscription is null || client.Subscription < DateTime.Now)
            {
                var responseFalse = new LogInLoginResponse() { ID = random.Next(1, 100000), ClientID = client.ID, IsSubscribed = false };
                return responseFalse;
            }*/
            
            var tokn = CreateToken(account).ToString();
            
            return new LogInLoginResponse() { ID = random.Next(1, 100000), ClientID = client.ID, IsSubscribed = true, Token = tokn };
        }
        private TokenResponseDto CreateTokenResponse(Account? user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
            };
        }

        
        #endregion

        #region create
        /// <summary>
        /// Funkcja pozwala na stworzzenie konta użytkownika i wysyła e-mail z danymi logowania na konto mailowe podane podczas kreacji
        /// </summary>
        /// <param name="accountCreate"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Response CreateAccount(AccountCreateDto accountCreate, int clientID, string mail)
        {
            const string litery = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            var responseNull = new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            if (accountCreate == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            var client = _dbContext.Clients.Include(p => p.Accounts).FirstOrDefault(x => x.ID == clientID);
            if (client == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Klient nie istnieje!" };

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
                RoleID = accountCreate.RoleId,
                FirstName = accountCreate.FirstName,
                LastName = accountCreate.LastName,
                ClientID = clientID
            };

            if(client.Accounts == null)
            {
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błąd - brak kont!" };
            }

            client.Accounts.Add(newAccount);
            _dbContext.SaveChanges();

            var creds = new AccountCredentials()
            {
                Login = login,
                Password = password.ToString(),
            };

            var message = new EmailMessage
            {
                To = [mail],
                Subject = "Dane Logowania",
                Body = $"Login: {login}\n Hasło: {password}"
            };
            _mailingService.SendEmail(message);

            return new AccountCredentialsResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = creds };
        }

        private string CreateToken(Account user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.ClientID.ToString()),
                new Claim(ClaimTypes.Role, user.RoleID.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        #endregion

        #region Edit
        public Response EditAccount(AccountEditDto accountEdit, int clientID)
        {
            
            var account = _dbContext.Accounts.FirstOrDefault(x => x.ID == accountEdit.Id);
            if (account == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };

            if (account.ClientID != clientID)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Brak uprawnień do konta" };

            account.WarehouseID = accountEdit.WarehouseID;
            account.RoleID = accountEdit.RoleId;
            account.FirstName = accountEdit.FirstName;
            account.LastName = accountEdit.LastName;
            _dbContext.SaveChanges();

            return new AccountEditResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = accountEdit };
        }

        public Response EditPassword(AccountEditPasswordDto editPasswordDto, int clientID)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.ID == clientID && x.Login == editPasswordDto.Login);
            if (account == null)
            {
                new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Błędnie podane dane" };
            }

            account.Password = getHashed(editPasswordDto.Password);
            _dbContext.SaveChanges();

            return new AccountEditPasswordResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = editPasswordDto };
        }
        #endregion

        #region Delete
        public Response DeleteAccount(int clientID, int ID)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.ID == ID);
            if (account == null)
            {
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiego modelu!" };
            }

            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();

            return new DeleteResponse() { ID = random.Next(1, 100000), ClientID = clientID, deleteMsg = "Konto zostało usunięte" };
        }

        #endregion
        public Response GetAllRoles(int clientID) 
        {
            var roleList = _dbContext.Roles.Where(x => x.RoleName != "Master" && x.RoleName != "HeadAdmin").ToList();
            if (roleList.Count() == 0)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma ról!" };

            return new AllRolesGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = roleList };
        }
        public Response GetRole(int clientID, int roleID)   
        {
            var role = _dbContext.Roles.Where(x => x.ID == roleID).FirstOrDefault();
            if (role == null)
                return new ErrorResponse() { ID = random.Next(1, 100000), ClientID = clientID, errorMsg = "Nie ma takiej roli!" };
            
            return new RoleGetResponse() { ID = random.Next(1, 100000), ClientID = clientID, Model = role };
        }
    }
}
