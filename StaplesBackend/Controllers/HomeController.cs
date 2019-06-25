using Microsoft.AspNetCore.Mvc;


namespace StaplesBackend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
