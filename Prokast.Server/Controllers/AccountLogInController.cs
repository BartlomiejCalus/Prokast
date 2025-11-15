using Prokast.Server.Entities;
using Prokast.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models.ResponseModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Prokast.Server.Services.Interfaces;
using Prokast.Server.Models.AccountModels;
using Prokast.Server.Models.ResponseModels.AccountResponseModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Prokast.Server.Models.JWT;
using Microsoft.AspNetCore.Authorization;

namespace Prokast.Server.Controllers
{
    [Route("api/login")]
    public class AccountController : ControllerBase
    {
        private readonly ILogInService _LogInService;

        public AccountController(ILogInService logInService)
        {
            _LogInService = logInService;
        }

        private int GetClientIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Token nie zawiera ClientID!");

            return int.Parse(claim.Value);
        }

        #region LogIn
        [HttpPost]
        [ProducesResponseType(typeof(LogInLoginResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<TokenResponseDto> Log_In([FromBody] LoginRequest loginRequest) 
        {            
            try 
            { 
                var response =  _LogInService.Log_In(loginRequest);
                if (response is ErrorResponse) return BadRequest(response);
                    return Ok(response);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAll
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(LogInGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAll() 
        {
            var clientIdFromToken = GetClientIdFromToken();
            try
            {
                var logins = _LogInService.GetLogIns(clientIdFromToken);
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
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> CreateAccount([FromBody] AccountCreateDto accountCreate)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _LogInService.CreateAccount(accountCreate, clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(AccountEditResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditAccount([FromBody] AccountEditDto accountEdit)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _LogInService.EditAccount(accountEdit, clientIdFromToken);
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
        [Authorize]
        [ProducesResponseType(typeof(AccountEditPasswordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> EditPassword([FromBody] AccountEditPasswordDto editPasswordDto)
        {
            var clientIdFromToken = GetClientIdFromToken();

            if (!ModelState.IsValid)
            {
                return BadRequest("Błędne dane");
            }
            try
            {
                var result = _LogInService.EditPassword(editPasswordDto, clientIdFromToken);
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
        [Authorize]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> DeleteAccount( [FromRoute] int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _LogInService.DeleteAccount(clientIdFromToken, ID);
                if (result is ErrorResponse) return BadRequest(result);

                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("authenticated")]
        [Authorize]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are and admin!");
        } 
    }   
}
