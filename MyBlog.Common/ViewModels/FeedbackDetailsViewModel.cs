using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common.ViewModels
{
    public class FeedbackDetailsViewModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public static Func<Feedback,FeedbackDetailsViewModel>FromFeedback
        {
            get
            {
                return feedback => new FeedbackDetailsViewModel()
                {
                    Message = feedback.Message,
                    Id = feedback.Id,
                    Author = feedback.Author,
                    AuthorId = feedback.AuthorId
                };
            }
        }
    }
}
