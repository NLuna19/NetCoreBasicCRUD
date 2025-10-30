using System.ComponentModel.DataAnnotations;

namespace BECommentsAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Author { get; set; }
        [Required]
        public required string Text { get; set; }
        public DateTime creationDate { get; set; }
    }
}
