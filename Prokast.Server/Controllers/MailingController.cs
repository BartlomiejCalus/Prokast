using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Prokast.Server.Controllers
{
    [Authorize]
    [Route("api/mailing")]
    public class MailingController: ControllerBase
    {
        private readonly IMailingService _mailingService;

        public MailingController(IMailingService mailingService)
        {
            _mailingService = mailingService;
        }


        [HttpPost]
        public ActionResult<Response> SendEmail([FromBody] EmailMessage emailMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mailingService.SendEmail(emailMessage);
            return Ok();
        }
    }
}
