using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Common;
using MyBlog.Data;

namespace MyBlog.Areas.Moderator.Controllers
{
    [Area(Constants.ModeratorRole)]
    [Authorize(Roles = Constants.AdministratorRole + ", " + Constants.ModeratorRole)]
    public abstract class ModeratorController : Controller
    {
        public BlogContext Context { get; set; }
        public ModeratorController(BlogContext context)
        {
            this.Context = context;
        }
    }
}