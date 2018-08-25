using MyBlog.Models;
using System;
using System.Collections.Generic;

namespace MyBlog.Common.ViewModels
{
    public class PostsHomeViewModel
    { 
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string CategoryBanner { get; set; }

        public DateTime DatePosted { get; set; }

        public static Func<Post, PostsHomeViewModel> FromPost
        {
            get
            {
                return post => new PostsHomeViewModel()
                {
                    Title = post.Title,
                    Body = post.Body,
                    DatePosted = post.DatePosted,
                    Id = post.Id,
                    CategoryBanner = post.Category.Banner
                };
            }
        }
    }
}