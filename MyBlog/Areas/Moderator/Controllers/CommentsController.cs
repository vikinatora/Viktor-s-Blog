using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Common;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Common.ViewModels;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Areas.Moderator.Controllers
{
    public class CommentsController : ModeratorController
    {
        private readonly INotificationSender notificationSender;
        private readonly UserManager<User> userManager;


        public CommentsController(BlogContext context, INotificationSender sender, UserManager<User> userManager)
            : base(context)
        {
            this.notificationSender = sender;
            this.userManager = userManager;
        }

        public IActionResult All(string id = null)
        {
            var comments = new List<Comment>();

            if (id == null)
            {
                comments = this.Context.Comments
                .Include(c => c.Author)
                .Take(30)
                .ToList();
            }
            else
            {
                comments = this.Context.Comments
                .Include(c => c.Author)
                .Where(c => c.AuthorId == id)
                .Take(30)
                .ToList();
            }


            var model = new List<CommentViewModel>();

            foreach (var comment in comments)
            {
                var commentModel = new[] { comment }
                                            .Select(CommentViewModel.FromComment)
                                            .First();

                if (userManager.IsInRoleAsync(comment.Author, Constants.AdministratorRole).Result)
                {
                    commentModel.IsAuthorAdmin = true;
                }

                model.Add(commentModel);
            }


            return View(model);
        }



        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var comment = this.Context.Comments.Find(id);

            if (comment == null)
            {
                return NotFound();
            }

            this.Context.Comments.Remove(comment);
            this.Context.SaveChanges();

            notificationSender.SendNotification(Constants.ModeratorDeletedCommentMessage, MessageType.Success, controller: this);

            return RedirectToAction("all", "comments", new { area = "moderator" });

        }
    }
}