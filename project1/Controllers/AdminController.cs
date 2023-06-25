using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project1.Models;
using project1.Repositories;
using System.Reflection;

namespace project1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index(int? id)
        {
            var CatList = _repository.GetCategories();
            ViewBag.CatList = CatList;
            if (id != null && id != 0 && id > 0)
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
        [HttpGet]
        public IActionResult CreateAnimal()
        {
            var CatList = _repository.GetCategories();
            IEnumerable<SelectListItem> cat = CatList.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString(),
            });
            ViewBag.CatList = cat;
            return View();
        }


        [HttpPost]
        public IActionResult CreateAnimal(Animal animal, IFormFile? formFile)
        {

            if (ModelState.IsValid)
            {
                string WWWPath = _webHostEnvironment.WebRootPath;
                if (formFile != null)
                {
                    string PicPath = Path.Combine(WWWPath, @"pics");
                    string midName = Guid.NewGuid().ToString();
                    string exten = Path.GetExtension(formFile.FileName);
                    if (animal.ImagePath != null)
                    {
                        string oldPath = Path.Combine(WWWPath, animal.ImagePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(PicPath, midName + exten), FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                    }
                    animal.ImagePath = @"\pics\" + midName + exten;

                }
                _repository.CreateAnimal(animal);
                var CatList = _repository.GetCategories();
                ViewBag.CatList = CatList;
                var AniList = _repository.GetAnimals();
                return View("Index", AniList);
            }
            return NotFound();
        }
        public IActionResult Delete(int? id)
        {
            var check = _repository.GetAnimals();
            int count = 0;
            foreach (var test in check)
            {
                if (test.AnimalId == id) { count++; }
            }
            if (count < 1) { return BadRequest(); }
            else
            {
                _repository.DeleteAnimal(id);
                var AniList = _repository.GetAnimals();
                var CatList = _repository.GetCategories();
                ViewBag.CatList = CatList;
                return View("Index", AniList);
            }

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var CatList = _repository.GetCategories();
            IEnumerable<SelectListItem> cat = CatList.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString(),
            });
            ViewBag.CatList = cat;
            var check = _repository.GetAnimals();
            int count = 0;
            foreach (var test in check)
            {
                if(test.AnimalId == id) { count++; }
            }
            if (count < 1) { return BadRequest(); }
            else
            {
                var animal = _repository.GetAnimalById(id!.Value);
                return View(animal);
            }
        }
        [HttpPost]
        public IActionResult Edit(Animal animal, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile == null)
                {
                    _repository.UpdateAnimal(animal);
                    var CatList = _repository.GetCategories();
                    ViewBag.CatList = CatList;
                    var AniList = _repository.GetAnimals();
                    return View("Index", AniList);
                }
                else
                {
                    string WWWPath = _webHostEnvironment.WebRootPath;
                    string PicPath = Path.Combine(WWWPath, @"pics");
                    string midName = Guid.NewGuid().ToString();
                    string exten = Path.GetExtension(formFile.FileName);
                    using (var fileStream = new FileStream(Path.Combine(PicPath, midName + exten), FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                    }
                    animal.ImagePath = @"\pics\" + midName + exten;
                    _repository.UpdateAnimal(animal);

                    var AniList = _repository.GetAnimals();
                    var CatList = _repository.GetCategories();
                    ViewBag.CatList = CatList;
                    return View("Index", AniList);
                }
            }
            return NotFound();
        }

    }
}
