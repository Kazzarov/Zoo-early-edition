using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using project1.Data;
using project1.Models;
using System.Linq;
using System.Net.Mail;

namespace project1.Repositories
{
    public class PetShopRepository : IRepository
    {

        private AnimalDbContext _context;
        public PetShopRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public void CreateAnimal(Animal animal)
        {
            _context.Animals!.Add(animal);
            _context.SaveChanges();
        }

        public void CreateCategory(Category category)
        {
            _context.Categories!.Add(category);
            _context.SaveChanges();
        }

        public void CreateComment(int id, Comment comment)
        {
            var animalInId = _context.Animals!.FirstOrDefault(a => a.AnimalId == id);
            if(animalInId!.AnimalId == comment.AnimalId)
            { 
            _context.Comments!.Add(comment);
            animalInId!.Comments!.Add(comment);
            _context.SaveChanges();

            }
        }

        public void DeleteAnimal(int? id)
        {
            var animal = _context.Animals!.Single(a => a.AnimalId == id);
            _context.Animals!.Remove(animal);
            _context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            var comment = _context.Comments!.FirstOrDefault(c => c.CommentId == id);
            _context.Comments!.Remove(comment!);
            _context.SaveChanges();
        }


        public Animal GetAnimalById(int id)
        {
            var animal = _context.Animals.FirstOrDefault(animal => animal.AnimalId == id);
            return animal!;
        }

        public IEnumerable<Category> GetCategoriesByName(string name)
        {
            return _context.Categories!.Where(c => c.Name == name);
        }

        public IEnumerable<Comment> GetComments()
        {
            return _context.Comments!;
        }

        public void UpdateAnimal(Animal animal)
        {
            _context.Animals.Update(animal);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            var categoryInDb = _context.Categories!.Single(c => c.CategoryId == category.CategoryId);
            categoryInDb.Name = category.Name;
        }

        public void UpdateComment(Comment comment)
        {
            var commentInDb = _context.Comments!.Single(c => c.CommentId == comment.CommentId);
            commentInDb.Content = comment.Content;
        }

        public string ErrorMsg(string msg)
        {
            return msg;
        }

        public IEnumerable<Comment> GetAnimalComments(int id)
        {
            var comments = _context.Comments.Where(c => c.AnimalId == id).ToList();
            return comments;
        }

        public IEnumerable<Animal> GetTopComment()
        {
            var topAnimals = _context.Animals.OrderByDescending(a => a.Comments!.Count())
                .Include(a => a.Comments).Take(2).ToList();
            return topAnimals;
        }

        public IEnumerable<Animal> GetAnimals()
        {
            return _context.Animals!.ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories!;
        }

        public IEnumerable<Animal> GetAnimalsByCategory(int catId)
        {
            var animals = _context.Animals.Where(a => a.CategoryId == catId).ToList();
            return animals;
        }
    }
}
