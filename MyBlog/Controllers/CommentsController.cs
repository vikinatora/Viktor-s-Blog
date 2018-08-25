using Microsoft.AspNetCore.Mvc;
using MyBlog.Common;
using MyBlog.Common.BindingModels;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Data;
using MyBlog.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace MyBlog.Controllers
{
    public class CommentsController : DbController
    {
        private readonly INotificationSender notificationSender;

        public CommentsController(BlogContext context, INotificationSender sender)
            : base(context)
        {
            this.notificationSender = sender;
        }

        public IActionResult Add(string message, int postId)
        {
            //Create an attribute for this
            if (this.User.Claims.Count() == 0)
            {
                return RedirectToAction("login", "account", new { area = "identity" });

            }

            if (string.IsNullOrWhiteSpace(message))
            {
                return RedirectToAction("index", "home");
            }

            var userId = this.User.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault()
                .Value;

            var comment = new Comment()
            {
                DatePosted = DateTime.Now,
                Message = message,
                AuthorId = userId,
                PostId = postId
            };

            this.Context.Comments.Add(comment);
            this.Context.SaveChanges();

            notificationSender.SendNotification(Constants.UserAddedComment, MessageType.Success, controller: this);

            return RedirectToAction("details", "posts", new { id = comment.PostId });
        }
    }
}