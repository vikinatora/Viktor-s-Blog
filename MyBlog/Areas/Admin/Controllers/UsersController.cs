using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Common;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Common.ViewModels;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly UserManager<User> userManager;
        private readonly INotificationSender notificationSender;

        public UsersController(BlogContext context, UserManager<User> userManager, INotificationSender sender)
            : base(context)
        {
            this.userManager = userManager;
            this.notificationSender = sender;
        }

        public IActionResult All()
        {
            var model = new List<UsersConciseViewModel>();
            var currentUser = this.userManager.GetUserAsync(this.User).Result;

            var users = this.Context.Users
                .Where(u => u.Id != currentUser.Id)
                .Include(u => u.Comments)
                .ToList();

            foreach (var user in users)
            {
                var userModel = new[] { user }
                                        .Select(UsersConciseViewModel.FromUser)
                                        .First();

                if (userManager.IsInRoleAsync(user, Constants.ModeratorRole).Result)
                {
                    userModel.IsModerator = true;
                }

                if(user.LockoutEnd !=null)
                {
                    userModel.IsBanned = true;
                }

                model.Add(userModel);
            }

            return View(model);
        }


        public async Task<IActionResult> MakeModerator(string id)
        {
            var user = this.Context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            await userManager.AddToRoleAsync(user, Constants.ModeratorRole);

            notificationSender.SendNotification(String.Format(Constants.UserPromotedMessage, user.UserName), MessageType.Success, controller: this);


            return RedirectToAction("All", new { area = "admin", controller = "users" });
        }

        public async Task<IActionResult> Demote(string id)
        {
            var user = this.Context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            await userManager.RemoveFromRoleAsync(user, Constants.ModeratorRole);

            notificationSender.SendNotification(String.Format(Constants.UserPromotedMessage, user.UserName), MessageType.Success, controller: this);


            return RedirectToAction("All", new { area = "admin", controller = "users" });
        }

        public async Task<IActionResult> Ban(string id)
        {
            var user = this.Context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            await this.userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddDays(7));

            this.Context.SaveChanges();

            return RedirectToAction("All", "users", new { area = "admin" });
        }

        public async Task<IActionResult> Unban(string id)
        {
            var user = this.Context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            await this.userManager.SetLockoutEndDateAsync(user, DateTime.Parse("Jan 1, 1999"));

            user.LockoutEnd = null;
            this.Context.SaveChanges();

            return RedirectToAction("All", "users", new { area = "admin" });
        }


        public string GetCurrentUsername()
        {
            return this.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)
                .Value;
        }
    }
}