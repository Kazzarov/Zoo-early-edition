using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project1.Models;
using project1.Repositories;

namespace project1.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IRepository _repository;
        public CatalogController(IRepository repository)
        {
            _repository= repository;
        }
        [HttpGet]
        public IActionResult MainPage(int? id)
        {
            var CatList = _repository.GetCategories();
            ViewBag.CatList = CatList;
            if(id != null && id != 0 && id > 0)
            {
                var sortList = _repository.GetAnimalsByCategory(id.Value);
                return View(sortList);
            }
            else
            {
                var AniList = _repository.GetAnimals();
                return View(AniList);
            }
        }
        public IActionResult AnimalPage(int? id) 
        {
            if (id != null && id != 0 && id > 0)
            {
                var SelectedAnimal = _repository.GetAnimalById(id.Value);
                if (SelectedAnimal != null)
                {
                    return View(SelectedAnimal);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult Comments(int? id) 
        {
            var comments = _repository.GetAnimalComments(id!.Value);
            ViewBag.AnimalId = id.Value;
            if(comments != null)
            {
            ViewBag.Comments = comments;
            return View();
            }
            else
            {
               return View();
            }
           
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment) 
        {
            var CatList = _repository.GetCategories();
            ViewBag.CatList = CatList;
            var AniList = _repository.GetAnimals();
            _repository.CreateComment(comment.AnimalId,comment);
            return View("MainPage", AniList);
            
        }
        public IActionResult DeleteComment(int id)
        {
            var CatList = _repository.GetCategories();
            ViewBag.CatList = CatList;
            var AniList = _repository.GetAnimals();
            _repository.DeleteComment(id);
            return View("MainPage", AniList);
        }
    }
}
