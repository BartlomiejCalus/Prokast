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
using Prokast.Server.Models.ResponseModels.CustomParamsResponseModels;
using Prokast.Server.Services;

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
                if (response is null) return BadRequest();
                    return Ok(response);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAll
        [HttpGet]
        [Authorize(Roles = "1,2,3,4,5")]
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
        [Authorize(Roles = "1,2,3,4,5")]
        [ProducesResponseType(typeof(AccountCredentialsResponse), StatusCodes.Status200OK)]
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
        [Authorize(Roles = "1,2,3,4,5")]
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
        [Authorize(Roles = "1,2,3,4,5")]
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
        [Authorize(Roles = "1,2,3,4,5")]
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

        [HttpGet("Role")]
        [Authorize(Roles = "1,2,3,4,5")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetAllRoles()
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _LogInService.GetAllRoles(clientIdFromToken);
                if (result is ErrorResponse) return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Role{ID}")]
        [Authorize(Roles = "1,2,3,4,5")]
        [ProducesResponseType(typeof(ParamsGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Response> GetRole([FromQuery]int ID)
        {
            var clientIdFromToken = GetClientIdFromToken();

            try
            {
                var result = _LogInService.GetRole(clientIdFromToken, ID);
                if (result is ErrorResponse) return BadRequest(result);
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
        [Authorize(Roles = "3")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are and admin!");
        } 
    }   
}
