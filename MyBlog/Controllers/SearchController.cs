using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Common.ViewModels;
using MyBlog.Data;

namespace MyBlog.Controllers
{
    public class SearchController : DbController
    {
        public SearchController(BlogContext context) : base(context)
        {
        }

        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return NotFound();
            }

            var model = new SearchViewModel
            {
                SearchTerm = searchTerm
            };

            var foundPosts = this.Context
                .Posts
                .Include(p => p.Comments)
                .Where(b => FindObjectWithRightName(b.Title, searchTerm))
                .Select(SearchDetailsViewModel.FromPost)
                .ToList();

            model.Results = foundPosts;

            foreach (var item in model.Results)
            {
                string markResult = HtmlizeTitle(searchTerm, item.PostTitle);

                item.PostTitle = markResult;
            }

            return View(model);

        }

        public string HtmlizeTitle(string searchTerm, string title)
        {
            return Regex.Replace(
                title,
                $"({Regex.Escape(searchTerm)})",
                match => $"<strong class=\"text-danger\">{match.Groups[0].Value}</strong>",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public bool FindObjectWithRightName(string name, string searchTerm)
        {
            return name.ToLower().Contains(searchTerm.ToLower());
        }
    }
}