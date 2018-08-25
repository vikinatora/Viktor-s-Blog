using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Common;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Data;
using MyBlog.Helpers;
using MyBlog.Models;

namespace MyBlog.Pages
{
    public class FeedbackModel : PageModel
    {
        private INotificationSender notificationSender;

        public FeedbackModel(BlogContext context, INotificationSender sender)
        {
            this.Context = context;
            this.notificationSender = sender;
        }
        public BlogContext Context { get; set; }


        [BindProperty]
        [Required]
        [MinLength(10)]
        public string Feedback { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostSend()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            if (!this.User.Claims.Any())
            {
                return RedirectToAction("login", "account", new { area = "identity" });
            }

            var userId = this.User.Claims
               .Where(c => c.Type == ClaimTypes.NameIdentifier)
               .FirstOrDefault()
               .Value;

            if (string.IsNullOrWhiteSpace(this.Feedback))
            {
                return this.Page();
            }

            var feedback = new Feedback()
            {
                Message = this.Feedback,
                AuthorId = userId
            };

            notificationSender.SendNotification(Constants.FeedbackSentMessage, MessageType.Success, pageModel: this);

            return RedirectToAction("index", "home");
        }


    }
}