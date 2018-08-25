using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Common.Constants;
using TicTacToe.Services.Interfaces;

namespace TicTacToeWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.PLAYER_ROLE)]
        public IActionResult All()
        {
            var model = userService.GetAllUsers();
            return View(model);
        }
    }
}