﻿using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;
using Prokast.Server.Services.Interfaces;

namespace Prokast.Server.Controllers
{
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
