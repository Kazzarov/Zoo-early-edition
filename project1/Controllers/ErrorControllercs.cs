using Microsoft.AspNetCore.Mvc;

namespace project1.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return Content("There was an unexpected error. Please try later");
        }
    }
}
