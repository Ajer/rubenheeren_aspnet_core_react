using System.ComponentModel.DataAnnotations;

namespace rubenheeren_aspnet_core_react.Data
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }    

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = String.Empty;

        [Required]
        [MaxLength(100000)]
        public string Content { get; set; } = String.Empty;  // some comments here

        public DateTime CreatedTime { get; set; }
    }
}
