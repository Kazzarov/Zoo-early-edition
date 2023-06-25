using Microsoft.AspNetCore.Mvc;
using project1.Repositories;

namespace project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        public HomeController(IRepository repository)
        {
            _repository= repository;
        }
        public IActionResult Index()
        {
            var top2 =_repository.GetTopComment();
            return View(top2);
        }
    }
}
