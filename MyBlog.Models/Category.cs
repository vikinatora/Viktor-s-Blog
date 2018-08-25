using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models
{
    public class Category
    {
        public Category()
        {
            this.Posts = new List<Post>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Url]
        [Required]
        public string Banner { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
