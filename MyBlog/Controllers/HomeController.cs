using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Common;
using MyBlog.Common.ViewModels;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class HomeController : DbController
    {
        public HomeController(BlogContext context)
            : base(context)
        {
        }

        public IActionResult Index(int id, string categoryFilter = null)
        {
            if (id == 0)
            {
                id = Constants.DefaultPage;
            }

            if (id <= 0)
            {
                return NotFound();
            }

            var model = new HomeViewModel
            {
                Page = id
            };

            double postsCount = 0;
            List<Post> posts = new List<Post>();

            if (categoryFilter == null)
            {
                posts = this.Context.Posts
                               .OrderByDescending(p => p.DatePosted)
                               .Include(p => p.Category)
                               .Include(p => p.Comments)
                               .ThenInclude(c => c.Author)
                               .Skip((id - 1) * Constants.DefaultPostsCountPerPage)
                               .Take(Constants.DefaultPostsCountPerPage)
                               .ToList();

                postsCount = this.Context.Posts.Count();
            }
            else
            {
                posts = this.Context.Posts
                                .Where(p => p.Category.Name.ToLower() == categoryFilter.ToLower())
                                .OrderByDescending(p => p.DatePosted)
                               .Include(p => p.Category)
                               .Include(p => p.Comments)
                               .ThenInclude(c => c.Author)
                               .Skip((id - 1) * Constants.DefaultPostsCountPerPage)
                               .Take(Constants.DefaultPostsCountPerPage)
                               .ToList();

                postsCount = this.Context.Posts.Where(p => p.Category.Name.ToLower() == categoryFilter.ToLower()).Count();
            }

            model.BlogPages = GetPagesCount(postsCount);

            var categories = this.Context
                .Categories
                .ToList();

            foreach (var post in posts)
            {
                post.Body = ShortenBody(post);

                var postViewModel = new[] { post }
                                            .Select(PostsHomeViewModel.FromPost)
                                            .First();

                model.Posts.Add(postViewModel);
            }

            foreach (var category in categories)
            {
                var categoryViewModel = new[] { category }
                                                .Select(CategoriesHomeViewModel.FromCategory)
                                                .First();

                model.Categories.Add(categoryViewModel);
            }

            return View(model);
        }

        public int GetPagesCount(double postsCount)
        {
            postsCount = postsCount <= 0 ? 1 : postsCount;

            double decimalPages = postsCount / Constants.DefaultPostsCountPerPage;
            var pages = (int)Math.Ceiling(decimalPages);
            return pages;
        }

        public string ShortenBody(Post post)
        {
            var cutBody = new StringBuilder();

            if (post.Body.Length > Constants.MaximalHomePagePostBodyLength)
            {
                cutBody.Append(post.Body.Substring(0, Constants.MaximalHomePagePostBodyLength))
                   .Append("...");
            }
            else
            {
                cutBody.Append(post.Body);
            }

            return cutBody.ToString();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
