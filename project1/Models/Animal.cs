using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }
        [Required (ErrorMessage ="Please enter a name for the animal")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter a Description for the animal")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please enter an Age for the animal")]
        public int Age { get; set; }
        [DisplayName("Picture")]
        public string? ImagePath { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int CommentId { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
