using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Areas.Admin.Controllers;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Common.ViewModels;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Tests.Admin
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void All_WithZeroUsers_ReturnsView()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();
            var users = new List<User>();
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(um => um.GetUserAsync(null))
                .ReturnsAsync(new User() { Id = "111" });

            var controller = new UsersController(dbContext, mockUserManager.Object, null);

            //Act
            var result = controller.All();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void All_WithSomeUsers_ReturnsView()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();
            var users = new List<User>()
            {
                new User(){Id = "a"},
                new User(){Id = "b"},
                new User(){Id = "c"},
                new User(){Id = "d"}
            };
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);

            var controller = new UsersController(dbContext, mockUserManager.Object, null);


            mockUserManager.Setup(um => um.GetUserAsync(null))
                .ReturnsAsync(users[1]);
            //Act
            var result = controller.All();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void All_ShouldReturnAllUsersExceptCurrent()
        {
            //Arrange
            var users = new[]
            {
                new User(){Id = "111"},
                new User(){Id = "222"},
                new User(){Id = "333"},
                new User(){Id = "444"},
            };

            var mockDbContext = new MockDbContext().GetContext();
            mockDbContext.Users.AddRange(users);
            mockDbContext.SaveChanges();

            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(um => um.GetUserAsync(null))
                .ReturnsAsync(users[1]);

            var controller = new UsersController(mockDbContext, mockUserManager.Object, null);


            //Act
            var result = controller.All() as ViewResult;

            //Assert
            var model = result.Model as IEnumerable<UsersConciseViewModel>;

            CollectionAssert.AreEqual(new[] { "111", "333", "444" }, model.Select(u => u.Id).ToArray());
        }

        [TestMethod]
        public void MakeModerator_WithInvalidId_ShouldReturnNotFound()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();
            dbContext.Users.Add(new User() { Id = "aaaaaaa" });
            dbContext.SaveChanges();
            var controller = new UsersController(dbContext, null, null);

            //Act
            var result = controller.MakeModerator("bbbbbbbb").Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void MakeModerator_WithValidId_ShouldRedirectToAdminUsersAll()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();
            var user = new User() { Id = "aaaaaaa", UserName = "aaaaaaaaa0" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);

            var notificationSender = new Mock<INotificationSender>();

            notificationSender.Setup(ns => ns.SendNotification(It.IsAny<string>(), It.IsAny<MessageType>(), It.IsAny<Controller>(), It.IsAny<PageModel>()))
                .Returns(true);

            var controller = new UsersController(dbContext, mockUserManager.Object, notificationSender.Object);

            //Act
            var result = controller.MakeModerator("aaaaaaa").Result as RedirectToActionResult;

            //Assert
            Assert.AreEqual("admin", result.RouteValues["area"]);
            Assert.AreEqual("users", result.RouteValues["controller"]);
            Assert.AreEqual("All", result.ActionName);


        }

        [TestMethod]
        public void Demote_WithInvalidId_ShouldReturnNotFound()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();
            dbContext.Users.Add(new User() { Id = "aaaaaaa" });
            dbContext.SaveChanges();
            var controller = new UsersController(dbContext, null, null);

            //Act
            var result = controller.Demote("bbbbbbbb").Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Demote_WithValidId_ShouldRedirectToAdminUsersAll()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();
            var user = new User() { Id = "aaaaaaa", UserName = "aaaaaaaaa0" };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);


            var notificationSender = new Mock<INotificationSender>();

            notificationSender.Setup(ns => ns.SendNotification(It.IsAny<string>(), It.IsAny<MessageType>(), It.IsAny<Controller>(), It.IsAny<PageModel>()))
                .Returns(true);

            var controller = new UsersController(dbContext, mockUserManager.Object, notificationSender.Object);

            //Act
            var result = controller.Demote("aaaaaaa").Result as RedirectToActionResult;

            //Assert
            Assert.AreEqual("admin", result.RouteValues["area"]);
            Assert.AreEqual("users", result.RouteValues["controller"]);
            Assert.AreEqual("All", result.ActionName);


        }
    }
}
