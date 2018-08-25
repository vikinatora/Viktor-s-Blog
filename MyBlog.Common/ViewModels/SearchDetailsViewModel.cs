using MyBlog.Models;
using System;

namespace MyBlog.Common.ViewModels
{
    public class SearchDetailsViewModel
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public int CommentsCount { get; set; }

        public static Func<Post,SearchDetailsViewModel> FromPost
        {
            get
            {
                return post => new SearchDetailsViewModel()
                {
                    PostId = post.Id,
                    PostTitle = post.Title,
                    CommentsCount = post.Comments.Count
                };
            }

        }
    }
}