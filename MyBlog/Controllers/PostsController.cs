using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Common.ViewModels;
using MyBlog.Data;

namespace MyBlog.Controllers
{
    public class PostsController : DbController
    {
        public PostsController(BlogContext context)
            : base(context)
        {
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var post = this.Context.Posts
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            var comments = post.Comments
                                .Select(CommentViewModel.FromComment)
                                .ToList();

            var postModel = new[] { post }
                                .Select(PostsHomeViewModel.FromPost)
                                .First();

            var model = new PostDetailsViewModel()
            {
                Comments = comments,
                Post = postModel
            };

            return View(model);
        }
    }
}