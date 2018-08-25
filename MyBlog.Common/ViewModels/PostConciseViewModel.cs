using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common.ViewModels
{
    public class PostConciseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public DateTime DatePosted { get; set; }

        public static Func<Post,PostConciseViewModel> FromPost
        {
            get
            {
                return post => new PostConciseViewModel()
                {
                    Title = post.Title,
                    DatePosted = post.DatePosted,
                    Id = post.Id,
                    Category = post.Category.Name
                };
            }
        }
    }
}
