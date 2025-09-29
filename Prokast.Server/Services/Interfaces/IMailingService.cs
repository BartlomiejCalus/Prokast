using Microsoft.AspNetCore.Mvc;
using Prokast.Server.Models;

namespace Prokast.Server.Services.Interfaces
{
    public interface IMailingService
    {
        void SendEmail(EmailMessage emailMessage);
    }
}
