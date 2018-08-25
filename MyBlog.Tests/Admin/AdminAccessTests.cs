using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Areas.Admin.Controllers;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MyBlog.Tests.Admin
{
    [TestClass]
    public class AdminAccessTests
    {
        [TestMethod]
        public void PostsController_SholdBeInAdminArea()
        {
            var area = typeof(PostsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AreaAttribute) as AreaAttribute;


            Assert.IsNotNull(area);
            Assert.AreEqual(Common.Constants.AdminArea, area.RouteValue);
        }

        [TestMethod]
        public void PostsController_ShouldBeAccessedByAdmins()
        {
            var authorization = typeof(PostsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            var roles = authorization.Roles.Split(",");
            var adminRole = roles[0];
            Assert.IsNotNull(adminRole);
            Assert.AreEqual(Common.Constants.AdministratorRole, adminRole);
        }

        [TestMethod]
        public void PostsController_ShouldNotBeAccessedByModerator()
        {
            var authorization = typeof(PostsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            var roles = authorization.Roles.Split(",")[0].ToString();

            Assert.ThrowsException<AssertFailedException>(() => StringAssert.Contains(roles, Common.Constants.ModeratorRole));
        }

        [TestMethod]
        public void PostsController_ShouldNotBeAccessedByUser()
        {
            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(um => um.GetRolesAsync(null))
                .ReturnsAsync(new List<string>());

            var authorization = typeof(PostsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            var defaultUserRoles = mockUserManager.Object.GetRolesAsync(null).Result;
            var roles = authorization.Roles.ToString();

            Assert.ThrowsException<AssertFailedException>(() => StringAssert.Contains(defaultUserRoles.ToString(), roles));
        }

        [TestMethod]
        public void PostsController_ShouldBeAccessibleByAdmin()
        {
            var controller = new UsersController(null, null, null);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Role, Common.Constants.AdministratorRole)
                    }))
                }
            };

            Assert.IsTrue(controller.User.IsInRole(Common.Constants.AdministratorRole));
        }
    }
}
