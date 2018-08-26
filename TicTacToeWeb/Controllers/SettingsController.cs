using Microsoft.AspNetCore.Mvc;

namespace TicTacToeWeb.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
