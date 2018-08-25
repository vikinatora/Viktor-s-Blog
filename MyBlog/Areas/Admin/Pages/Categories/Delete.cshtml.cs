using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Common;
using MyBlog.Data;

namespace MyBlog.Areas.Admin.Pages.Categories
{
    [Authorize(Roles = Constants.AdministratorRole)]

    public class DeleteModel : PageModel
    {
        public DeleteModel(BlogContext context)
        {
            this.Context = context;
        }

        public BlogContext Context { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }

        public void OnGet(int id)
        {
            var category = this.Context.Categories.Find(id);

            if (category == null)
            {
                NotFound();
            }

            this.Name = category.Name;
            this.Id = category.Id;

        }

        public ActionResult OnPostDelete(int id)
        {
            var category = this.Context.Categories.Find(id);
            this.Context.Categories.Remove(category);
            this.Context.SaveChanges();
            return Redirect("/home/index");
        }
    }
}