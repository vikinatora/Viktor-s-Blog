using MyBlog.Models;
using System;

namespace MyBlog.Common.ViewModels
{
    public class AuthorViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public static Func<User,AuthorViewModel> FromAuthor
        {
            get
            {
                return author => new AuthorViewModel()
                {
                    Id = author.Id,
                    Username = author.UserName
                };
            }
        }
    }
}