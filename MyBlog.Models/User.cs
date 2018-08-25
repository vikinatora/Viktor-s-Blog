using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyBlog.Models
{
    public class User:IdentityUser
    {
        public User()
        {
            this.Comments = new List<Comment>();
            this.Feedbacks = new List<Feedback>();
        }

        public string Avatar { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
    }
}