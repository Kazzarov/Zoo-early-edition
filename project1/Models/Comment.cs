using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }
        [Required]
        public string? Content { get; set; }
        
    }
}
