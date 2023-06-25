using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
