using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Services;

namespace TicTacToeWeb.Pages
{
    public class FeedbackModel : PageModel
    {
        private SendGridEmailService sendGrid;

        public FeedbackModel(SendGridEmailService sendGrid)
        {
            this.sendGrid = sendGrid;
        }

        [BindProperty]
        public string Subject { get; set; }

        [BindProperty]
        public string Content { get; set; }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            await sendGrid.SendEmailAsync(this.User.Identity.GetUserName(), this.Subject, this.Content);
        }
    }
}