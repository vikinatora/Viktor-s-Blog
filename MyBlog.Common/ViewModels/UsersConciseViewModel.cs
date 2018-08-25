using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common.ViewModels
{
    public class UsersConciseViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public int CommentsCount { get; set; }

        public string Username { get; set; }

        public bool IsModerator { get; set; }

        public static Func<User, UsersConciseViewModel> FromUser
        {
            get
            {
                return user => new UsersConciseViewModel()
                {
                    Username = user.UserName,
                    CommentsCount = user.Comments.Count,
                    Email = user.Email,
                    Id = user.Id,
                    IsModerator = false
                };

            }
        }

        public bool IsBanned { get; set; }
    }
}
