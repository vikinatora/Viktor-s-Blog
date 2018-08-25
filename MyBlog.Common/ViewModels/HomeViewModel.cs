using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.Posts = new List<PostsHomeViewModel>();
            this.Categories = new List<CategoriesHomeViewModel>();
        }

        public int Page { get; set; }

        public int BlogPages { get; set; }

        public ICollection<PostsHomeViewModel> Posts { get; set; }

        public ICollection<CategoriesHomeViewModel> Categories { get; set; }
    }
}
