using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBlog.Models
{
    public class Feedback
    {
        public Feedback()
        {
            this.IsRead = false;
        }

        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public bool IsRead { get; set; }
    }
}
