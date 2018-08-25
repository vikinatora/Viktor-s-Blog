using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Common;
using MyBlog.Data;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area(Constants.AdminArea)]
    [Authorize(Roles = Constants.AdministratorRole)]
    public abstract class AdminController : Controller
    {
        public BlogContext Context { get; set; }

        public AdminController(BlogContext context)
        {
            this.Context = context;
        }
    }
}