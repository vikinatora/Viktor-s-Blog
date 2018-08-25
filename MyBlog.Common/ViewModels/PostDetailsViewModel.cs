using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common.ViewModels
{
    public class PostDetailsViewModel
    {
        public PostDetailsViewModel()
        {
            this.Comments = new List<CommentViewModel>();
        }

        public PostsHomeViewModel Post { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

    }
}
