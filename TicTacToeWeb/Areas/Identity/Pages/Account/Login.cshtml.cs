using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Security;
using TicTacToe.Common.Constants;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;

namespace TicTacToeWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        Microsoft.AspNetCore.Identity.UserManager<User> userMangaer;
        private readonly ILogger<LoginModel> logger;
        private readonly IUserService userServiace;

        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger, IUserService userService, Microsoft.AspNetCore.Identity.UserManager<User> userMangaer)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.userServiace = userService;
            this.userMangaer = userMangaer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    var user = await userMangaer.FindByNameAsync(Input.Email);
                    var admin = await this.userMangaer.IsInRoleAsync(user, RoleConstants.ADMIN_ROLE);
                    if (admin)
                    {
                        return RedirectToAction("Index", "Users", new {area = "Admin"});
                    }
                    logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (context.HandlerMethod.MethodInfo.Name == "OnGetAsync" && signInManager.IsSignedIn(this.User))
            {
                string url;
                if (this.User.IsInRole(RoleConstants.PLAYER_ROLE))
                {
                    url = Url.Action("Index", "Game", new { area = "" });
                }
                else
                {
                    url = Url.Action("Index", "Users", new { area = "Admin" });
                }
                Response.Redirect(url);

            }
            base.OnPageHandlerExecuting(context);
        }
    }
}
