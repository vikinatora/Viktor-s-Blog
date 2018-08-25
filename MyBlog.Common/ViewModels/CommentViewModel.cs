using MyBlog.Models;
using System;

namespace MyBlog.Common.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Avatar { get; set; }

        public DateTime DatePosted { get; set; }

        public bool IsAuthorAdmin { get; set; }


        public static Func<Comment, CommentViewModel> FromComment
        {
            get
            {
                return comment => new CommentViewModel()
                {
                    AuthorId = comment.AuthorId,
                    Author = comment.Author.UserName,
                    Avatar = comment.Author.Avatar,
                    DatePosted = comment.DatePosted,
                    Id = comment.Id,
                    Message = comment.Message
                };
            }
        }

    }
}