using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Common.ViewModels;
using MyBlog.Data;

namespace MyBlog.Areas.Admin.Controllers
{
    public class FeedbackController : AdminController
    {
        public FeedbackController(BlogContext context)
            : base(context)
        {
        }

        public IActionResult All()
        {
            var model = new FeedbackViewModel
            {
                New = this.Context.Feedbacks.Where(f => !f.IsRead)
                                              .Include(f => f.Author)
                                              .Select(FeedbackDetailsViewModel.FromFeedback)
                                              .OrderByDescending(f => f.Id)
                                              .ToList(),

                Read = this.Context.Feedbacks.Where(f => f.IsRead)
                                               .Include(f => f.Author)
                                               .Select(FeedbackDetailsViewModel.FromFeedback)
                                               .OrderByDescending(f => f.Id)
                                               .ToList()
            };

            return View(model);
        }

        public IActionResult ChangeFeedbackStatus(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var feedback = this.Context.Feedbacks.Find(id);

            if (feedback == null)
            {
                return NotFound();
            }

            if (feedback.IsRead)
            {
                feedback.IsRead = false;
            }
            else if (!feedback.IsRead)
            {
                feedback.IsRead = true;
            }
            this.Context.Feedbacks.Update(feedback);
            this.Context.SaveChanges();

            return RedirectToAction("all", "feedback", new { area = "admin" });
        }
    }
}