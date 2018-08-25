using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models
{
    public class Post
    {
        public Post()
        {
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int Likes { get; set; }

    }
}
