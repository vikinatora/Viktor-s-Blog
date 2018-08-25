using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Data;
using MyBlog.Common;
using MyBlog.Models;
using MyBlog.Common.Utilities;
using MyBlog.Common.ExtensionModels;
using System;
using System.ComponentModel.DataAnnotations;
using MyBlog.Helpers.Utilities;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace MyBlog.Areas.Admin.Pages.Categories
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class CreateModel : PageModel
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly INotificationSender notificationSender;

        public CreateModel(BlogContext context, IHostingEnvironment hostingEnvironment,INotificationSender sender)
        {
            this.Context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.notificationSender = sender;
        }
        public BlogContext Context { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [Url]
        [Required]
        [BindProperty]
        [Display(Name = Constants.BannerUrlDisplayName)]
        public string BannerUrl { get; set; }

        public void OnGet(BlogContext context)
        {

        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            var category = this.Context.Categories.FirstOrDefault(c => c.Name == this.Name);

            if (category != null)
            {
                notificationSender.SendNotification(String.Format(Constants.CategoryAlreadyExistsMessage, category.Name), MessageType.Danger, pageModel: this);
                return this.Page();
            }
            var bannerExtension = BannerUrl.Substring(BannerUrl.LastIndexOf('.'));

            var bannerFileName = String.Format(Constants.CategoryBannerName, this.Name, bannerExtension);

            var wwwRootPath = this.hostingEnvironment.WebRootPath;

            var bannerPath = wwwRootPath + Constants.CategoriesBannerPathFromWebRoot + bannerFileName;

            ImageUtilities.SaveCategoryBannerFromUrl(bannerPath, this.BannerUrl);

            category = new Category()
            {
                Name = this.Name,
                Banner = bannerFileName
            };

            this.Context.Categories.Add(category);
            this.Context.SaveChangesAsync();

            notificationSender.SendNotification(String.Format(Constants.CategoryCreatedMessage, category.Name), MessageType.Success, pageModel: this);

            return this.Redirect("/home/index");
        }
    }
}