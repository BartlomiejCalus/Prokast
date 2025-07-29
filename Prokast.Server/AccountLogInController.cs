//using AutoMapper;
using Prokast.Server.Entities;
using Prokast.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.AccountModels;
using Prokast.Server.Models.ResponseModels.AccountResponseModels;
//using Microsoft.EntityFrameworkCore;


namespace Prokast.Server.Controllers
{
    [Route("api/login")]
    [Tags("Accounts")]
    public class AccountController : ControllerBase
    {
        private readonly ILogInService _LogInService;



        public AccountController(ILogInService logInService)
        {
            _LogInService = logInService;
        }

        #region LogIn
        [HttpPost]
        [EndpointSummary("Login")]
        [ProducesResponseType(typeof(LogInLoginResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation for login in. Login in the form of an email for the client, and in the form of a generated name for the employee.")]
        public ActionResult<Response> Log_In([FromBody] LoginRequest loginRequest) 
        {
            try 
            { 
                var response = _LogInService.Log_In(loginRequest);
                if (response is ErrorResponse) return BadRequest(response);
                return Ok(response);
            }catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAll
        [HttpGet]
        [EndpointSummary("Get all accounts")]
        [ProducesResponseType(typeof(LogInGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A GET operation. Endpoint returns all accounts assigned to the client, both employees and the client him/herself.")]
        public ActionResult<Response> GetAll([FromQuery] int clientID) 
        {
            try
            {
                var logins = _LogInService.GetLogIns(clientID);
                if (logins is ErrorResponse) return BadRequest(logins);
                return Ok(logins);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }   
        }
        #endregion

        [HttpPost("create")]
        [EndpointSummary("Create an employee account")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint generates login and password based on new employee's personal data and creates an account for him.")]
        public ActionResult<Response> CreateAccount([FromBody] AccountCreateDto accountCreate,[FromQuery] int clientID)
        {
            try
            {
                var result = _LogInService.CreateAccount(accountCreate, clientID);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [EndpointSummary("Edit account details")]
        [ProducesResponseType(typeof(AccountEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint edits data of a given account.")]
        public ActionResult<Response> EditAccount([FromBody] AccountEditDto accountEdit, [FromQuery] int clientID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _LogInService.EditAccount(accountEdit, clientID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Password")]
        [EndpointSummary("Change password")]
        [ProducesResponseType(typeof(AccountEditPasswordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A PUT operation. Endpoint made specifically to change an account's password.")]
        public ActionResult<Response> EditPassword([FromBody] AccountEditPasswordDto editPasswordDto, [FromQuery] int clientID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _LogInService.EditPassword(editPasswordDto, clientID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")]
        [EndpointSummary("Delete an account")]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A DELETE operation. Endpoint deletes a given account.")]
        public ActionResult<Response> DeleteAccount([FromQuery] int clientID, [FromRoute] int ID)
        {
            try
            {
                var result = _LogInService.DeleteAccount(clientID, ID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
