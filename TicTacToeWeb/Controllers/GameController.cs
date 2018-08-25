using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicTacToe.Common.BindingModels;
using TicTacToe.Common.Constants;
using TicTacToe.Common.ViewModels;
using TicTacToe.Services.Exceptions;
using TicTacToe.Services.Interfaces;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace TicTacToeWeb.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();
            var viewModel = new GameIndexViewModel()
            {
                AvailableGames = this.gameService.GetAvailableGames(userId),
                UserGamesInProgress = this.gameService.GetUserGamesInProgress(userId),
                UserJoinedGames = this.gameService.GetUserJoinedGames(userId)
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new CreateGameBindingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGameBindingModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var createdGame = this.gameService.Create(input, this.User.Identity.GetUserId());
            return RedirectToAction(nameof(Play), new { Id = createdGame.Id });
        }

        [HttpGet]
        public IActionResult Join(Guid id)
        {
            try
            {
                this.gameService.Join(new GameJoinBindingModel { GameId = id }, User.Identity.GetUserId());

                return RedirectToAction(nameof(Play), new { id = id });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error", "", new { area = "Identity" });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Join(GameJoinBindingModel input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ValidationException(ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage);
                }

                this.gameService.Join(input, this.User.Identity.GetUserId());

                return this.Json(new { Success = true });
            }
            catch (Exception e)
            {
                // TODO: catches
                var exceptionMessage = e is ValidationException || e is NotFoundException ? e.Message : "An error occured";
                if (e is ValidationException || e is NotFoundException)
                {
                    exceptionMessage = e.Message;
                }
                return this.Json(new { Success = false, Exception = exceptionMessage });
            }
        }

        [HttpGet]
        public IActionResult Play(Guid id)
        {
            try
            {
                var userId = this.User.Identity.GetUserId();

                var statusGame = this.gameService.Status(id, userId);
                statusGame.PrivateJoinLink = Url.Action("Join", "Game", new {id = statusGame.Id}, Request.Scheme);

                return View(statusGame);
            }
            catch (Exception)
            {
                return RedirectToPage("/Error", "", new { area = "Identity" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Play(PlayGameBindingModel input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ValidationException(ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage);
                }

                var userId = User.Identity.GetUserId();
                this.gameService.Play(input.GameId, userId, input.Row, input.Col);
                return this.Json(new { Success = true });
            }
            catch (Exception e)
            {
                var exceptionMessage = e is ValidationException || e is NotFoundException ? e.Message : "An error occured";

                return this.Json(new { Success = false, Exception = exceptionMessage });
            }
        }

        [HttpGet]
        public IActionResult Status(Guid id)
        {
            try
            {
                var status = gameService.Status(id, User.Identity.GetUserId());

                return this.Json(new { Success = true, status });
            }
            catch (Exception e)
            {
                var exceptionMessage = e is ValidationException || e is NotFoundException ? e.Message : "An error occured";

                return this.Json(new { Success = false, Exception = exceptionMessage });
            }
        }

        [HttpGet]
        public IActionResult GameList()
        {
            var gameList = new GameIndexViewModel()
            {
                AvailableGames = this.gameService.GetAvailableGames(this.User.Identity.GetUserId()),
                UserGamesInProgress = this.gameService.GetUserGamesInProgress(this.User.Identity.GetUserId()),
                UserJoinedGames = this.gameService.GetUserJoinedGames(this.User.Identity.GetUserId())
            };

            return this.PartialView("_GamesPartial", gameList);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
                if (this.User.IsInRole(RoleConstants.ADMIN_ROLE))
                {
                    string url = Url.Action("Index", "Users", new { area = "Admin" });
                    Response.Redirect(url);
                }
                else
                {
                    base.OnActionExecuting(context);
                }
        }
    }
}