using MyBlog.Models;
using System;

namespace MyBlog.Common.ViewModels
{
    public class CategoriesHomeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public static Func<Category, CategoriesHomeViewModel> FromCategory
        {
            get
            {
                return category => new CategoriesHomeViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
        }
    }
}