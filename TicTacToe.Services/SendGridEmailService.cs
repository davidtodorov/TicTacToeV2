using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TicTacToe.Services
{
    public class SendGridEmailService: IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(email, email);
            var to = new EmailAddress("t_david@students.softuni.bg", "Admin");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);
            var statusCode = response.StatusCode;
        }
    }
}
