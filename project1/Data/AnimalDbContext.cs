using Microsoft.EntityFrameworkCore;
using project1.Models;

namespace project1.Data
{
    public class AnimalDbContext : DbContext
    {
        public AnimalDbContext()
        {

        }
        public AnimalDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>().HasData(
                new Animal { AnimalId = 1, Name = "Hawk", Age = 2, Description = "Bird", CategoryId = 1, ImagePath = "/pics/Hawk.jpg"},
                new Animal { AnimalId = 2, Name = "Pekinez", Age = 4, Description = "Dog", CategoryId = 2 ,ImagePath = "/pics/pekinez.jpg" },
                new Animal { AnimalId = 3, Name = "Dolphin", Age = 20, Description = "Marine Life", CategoryId = 3, ImagePath = "/pics/dolphin.jpg" },
                new Animal { AnimalId = 4, Name = "Eagle", Age = 5, Description = "Bird", CategoryId = 1, ImagePath = "/pics/eagle.jpg" },
                new Animal { AnimalId = 5, Name = "Frog", Age = 2, Description = "Amphibian", CategoryId = 3, ImagePath = "/pics/frog.jpg" },
                new Animal { AnimalId = 6, Name = "Puma", Age = 10, Description = "Feline", CategoryId = 2, ImagePath = "/pics/puma.jpg" }

                );
            modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Flying" },
            new Category { CategoryId = 2, Name = "Land-Life" },
            new Category { CategoryId = 3, Name = "Water-life" }

            );
            modelBuilder.Entity<Comment>().HasData(
            new Comment { CommentId = 1, AnimalId = 1, Content = "Nice" },
            new Comment { CommentId = 2, AnimalId = 1, Content = "Nice bird" },
            new Comment { CommentId = 3, AnimalId = 6, Content = "Great" },
            new Comment { CommentId = 4, AnimalId = 6, Content = "Beautiful" },
            new Comment { CommentId = 5, AnimalId = 3, Content = "Lol" }


                );
        }
    }
}
