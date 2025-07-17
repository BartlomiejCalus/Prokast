using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Models.ClientModels;
using Prokast.Server.Models.ResponseModels;
using Prokast.Server.Services.Interfaces;
namespace Prokast.Server.Controllers
{
    [Route("api/client")]
    [Tags("Clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        #region RegisterClient
        [HttpPost]
        [EndpointSummary("Register Client")]
        [ProducesResponseType(typeof(ClientRegisterResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [EndpointDescription("A POST operation. Endpoint registers the client into the database.")]
        public ActionResult<Response> RegisterClient([FromBody] Registration registration)
        {
            try 
            {
                var response = _clientService.RegisterClient(registration);
                if (response is ErrorResponse) return BadRequest(response);
                return Created();
            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}