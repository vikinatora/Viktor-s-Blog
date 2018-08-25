using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Areas.Moderator.Controllers;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Models;
using MyBlog.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Tests.Moderator
{
    [TestClass]
    public class CommentsControllerTests
    {
        [TestMethod]
        public void All_WithNoComments_ShouldReturnView()
        {
            var dbContext = new MockDbContext().GetContext();

            var controller = new CommentsController(dbContext,null,null);

            var result = controller.All() as ViewResult;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void All_WithSomeComments_ShouldReturnView()
        {
            var dbContext = new MockDbContext().GetContext();
            var author = new User() { Id = "1" };
            dbContext.Comments.AddRange(new List<Comment>()
            {
                new Comment(){Message = "tralala",Author = author},
                new Comment(){Message = "asdf",Author = author },
                new Comment(){Message = "beep", Author = author }
            });
            dbContext.SaveChanges();

            var controller = new CommentsController(dbContext, null, null);

            var result = controller.All() as ViewResult;

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Delete_WithZeroOrNegativeId_ShouldReturnNotFound()
        {
            var dbContext = new MockDbContext().GetContext();

            var controller = new CommentsController(dbContext,null, null);

            var result = controller.Delete(-3);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Delete_WithInvalidId_ShouldReturnNotFound()
        {
            var dbContext = new MockDbContext().GetContext();
            dbContext.Comments.Add(new Comment() { Id = 2 });
            var controller = new CommentsController(dbContext, null, null);

            var result = controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Delete_WithValidId_ShouldDelete()
        {
            var dbContext = new MockDbContext().GetContext();

            var author = new User() { Id = "1" };
            dbContext.Comments.Add(new Comment() { Id = 2, Author = author });

            var controller = new CommentsController(dbContext, null, null);

            var result = controller.Delete(1);

            Assert.AreEqual(0, dbContext.Comments.Count());
        }

        [TestMethod]
        public void Delete_WithValidId_ShouldRedirectToModeratorCommentsAll()
        {
            var dbContext = new MockDbContext().GetContext();

            var author = new User() { Id = "1" };
            dbContext.Comments.Add(new Comment() { Id = 2, Author = author });

            var notificationSender = new Mock<INotificationSender>();

            notificationSender.Setup(ns => ns.SendNotification(It.IsAny<string>(), It.IsAny<MessageType>(), It.IsAny<Controller>(), It.IsAny<PageModel>()))
                .Returns(true);

            var controller = new CommentsController(dbContext, notificationSender.Object, null);

            var result = controller.Delete(2) as RedirectToActionResult;

            Assert.AreEqual(result.ActionName,"all");
            Assert.AreEqual(result.ControllerName, "comments");
            Assert.AreEqual(result.RouteValues["area"], "moderator");

        }
    }
}
