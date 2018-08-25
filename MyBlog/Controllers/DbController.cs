using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;

namespace MyBlog.Controllers
{
    public abstract class DbController : Controller
    {
        public BlogContext Context { get; set; }

        public DbController(BlogContext context)
        {
            this.Context = context;
        }
    }
}