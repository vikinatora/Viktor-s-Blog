using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Areas.Moderator.Controllers;
using MyBlog.Models;
using MyBlog.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Tests.Moderator
{
    [TestClass]
    public class ModeratorAccessTests
    {
        [TestMethod]
        public void CommentsController_ShouldBeInModeratorArea()
        {
            var area = typeof(CommentsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AreaAttribute) as AreaAttribute;


            Assert.IsNotNull(area);
            Assert.AreEqual(Common.Constants.ModeratorRole, area.RouteValue);
        }

        [TestMethod]
        public void CommentsController_ShouldBeAccessedByModerators()
        {
            var authorization = typeof(CommentsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            var roles = authorization.Roles.Split(", ");
            var moderatorRole = roles[1];
            Assert.IsNotNull(moderatorRole);
            Assert.AreEqual(Common.Constants.ModeratorRole, moderatorRole);
        }

        [TestMethod]
        public void CommentsController_ShouldBeAccessedByAdmins()
        {
            var authorization = typeof(CommentsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            var roles = authorization.Roles.Split(", ");
            var adminRole = roles[1];
            Assert.IsNotNull(adminRole);
            Assert.AreEqual(Common.Constants.ModeratorRole, adminRole);
        }

        [TestMethod]
        public void CommentsController_ShouldNotBeAccessedByUser()
        {
            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(um => um.GetRolesAsync(null))
                .ReturnsAsync(new List<string>());

            var authorization = typeof(CommentsController)
                        .GetCustomAttributes(true)
                        .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;
            var defaultUserRoles = mockUserManager.Object.GetRolesAsync(null).Result;
            var roles = authorization.Roles.ToString();

            Assert.ThrowsException<AssertFailedException>(() => StringAssert.Contains(defaultUserRoles.ToString(), roles));
        }
    }
}
