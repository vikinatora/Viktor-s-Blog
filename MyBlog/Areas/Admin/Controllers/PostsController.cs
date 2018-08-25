using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Common;
using MyBlog.Common.BindingModels;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Common.ViewModels;
using MyBlog.Data;
using MyBlog.Helpers;
using MyBlog.Models;

namespace MyBlog.Areas.Admin.Controllers
{
    public class PostsController : AdminController
    {
        private readonly NotificationSender notificactionSender;

        public PostsController(BlogContext context, INotificationSender sender)
            : base(context)
        {
            this.notificactionSender = new NotificationSender();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new PostCreationBindingModel
            {
                Categories = GetCategories()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult All()
        {
            var model = this.Context.Posts
                .Include(p=>p.Category)
                .Select(PostConciseViewModel.FromPost)
                .OrderByDescending(p=>p.DatePosted)
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.User.Claims
           .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
           .Value;

            var post = new Post()
            {
                Title = model.Title,
                Body = model.Body,
                AuthorId = userId,
                DatePosted = DateTime.Now,
                CategoryId = int.Parse(model.CategoryId)
            };

            this.Context.Posts.Add(post);

            await this.Context.SaveChangesAsync();

            notificactionSender.SendNotification(String.Format(Constants.PostCreatedMessage, post.Title), MessageType.Success, controller: this);


            return Redirect("/home/index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await this.Context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var model = new PostEditingBindingModel()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CategoryId = post.CategoryId.ToString(),
                Categories = GetCategories()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostEditingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = this.Context.Posts.Find(id);

            post.Title = model.Title;
            post.Body = model.Body;
            post.CategoryId = int.Parse(model.CategoryId);


            await this.Context.SaveChangesAsync();

            return Redirect("/home/index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await this.Context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var model = new PostEditingBindingModel()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CategoryId = post.CategoryId.ToString(),
                Categories = GetCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await this.Context.Posts.FindAsync(id);

            this.Context.Posts.Remove(post);
            await this.Context.SaveChangesAsync();

            return Redirect("/home/index");
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            return this.Context.Categories
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
        }
    }
}