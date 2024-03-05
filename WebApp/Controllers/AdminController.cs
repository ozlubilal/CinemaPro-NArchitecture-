using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
