using project1.Models;
namespace project1.Repositories
{
    public interface IRepository
    {
        IEnumerable<Animal> GetAnimals();
        IEnumerable<Animal> GetTopComment();
        IEnumerable<Animal> GetAnimalsByCategory(int catId);
        Animal GetAnimalById(int id);
        void CreateAnimal(Animal animal);
        void UpdateAnimal(Animal animal);
        void DeleteAnimal(int? id);

        IEnumerable<Category> GetCategoriesByName(string name);
        IEnumerable<Category> GetCategories();
        void CreateCategory(Category category);
        void UpdateCategory(Category category);

        IEnumerable<Comment> GetAnimalComments(int id);
        IEnumerable<Comment> GetComments();
        void CreateComment(int id,Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);

        string ErrorMsg(string s);
    }
}
