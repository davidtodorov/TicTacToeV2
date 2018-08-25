using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Common.BindingModels;
using TicTacToe.Models;
using TicTacToe.Services.Interfaces;

namespace TicTacToeWeb.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService userService;
        private readonly IAdminService adminService;
        private readonly IScoreService scoreSerice;
        private readonly IHistoryService historyService;

        public UsersController(IUserService userService, IAdminService adminService, IScoreService scoreService, IHistoryService historyService)
        {
            this.userService = userService;
            this.adminService = adminService;
            this.scoreSerice = scoreService;
            this.historyService = historyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //todo: if some of history/scores/users lists are empty, do not show them
            //todo: settings for therme
            //todo: add tempdata for message if user connect a game
            var adminId = this.User.Identity.GetUserId();
            var model = this.userService.GetAllUsers().Where(u => u.Id != adminId).ToList();
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Lock(string id)
        {
            await this.adminService.LockUser(id);
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Unlock(string id)
        {
            await this.adminService.UnlockUser(id);
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = this.userService.GetUserDetails(id);
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ResetScores(string id)
        {
            await this.scoreSerice.ResetScores(id);
            return Ok();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteHistory(DeleteHisotryBindingModel input)
        {
            this.historyService.DeleteHistory(input.Id, input.UserId);
            return Ok();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteAllHistory(string userId)
        {
            this.historyService.DeleteAllHistory(userId);
            return Ok();
        }
    }
}