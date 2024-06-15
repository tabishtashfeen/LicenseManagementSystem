using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Helper.Functions
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientName, string recipientEmail, string subject, string licenseKey, string userName);
    }
}
