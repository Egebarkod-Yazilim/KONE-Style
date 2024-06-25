using Microsoft.AspNetCore.Mvc;

namespace KONE.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
