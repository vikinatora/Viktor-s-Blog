using System;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

    }
}