using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBlog.Areas.Admin.Controllers;
using MyBlog.Models;
using MyBlog.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Tests.Admin
{
    [TestClass]
    public class FeedbackControllerTests
    {

        [TestMethod]
        public void Controller_ShouldBe_InAdminArea()
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
        public void All_WithNoFeedback_ShouldReturnView()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();

            var feedback = new List<Feedback>();
            dbContext.Feedbacks.AddRange(feedback);
            dbContext.SaveChanges();

            //Act
            var controller = new FeedbackController(dbContext);
            var result = controller.All();

            //Assert
            Assert.IsInstanceOfType(result as ViewResult, typeof(ViewResult));
        }

        [TestMethod]
        public void All_WithSomeFeedback_ShouldReturnView()
        {
            //Arrange
            var dbContext = new MockDbContext().GetContext();

            var feedback = new List<Feedback>()
            {
                new Feedback(){Id =1},
                new Feedback(){Id =2},
                new Feedback(){Id =3},
                new Feedback(){Id =4}
            };
            dbContext.Feedbacks.AddRange(feedback);
            dbContext.SaveChanges();

            //Act
            var controller = new FeedbackController(dbContext);
            var result = controller.All();

            //Assert
            Assert.IsInstanceOfType(result as ViewResult, typeof(ViewResult));
        }

        [TestMethod]
        public void ChangeFeedbackStatus_WithNegativeId_ShouldReturnNotFound()
        {

            //Act
            var controller = new FeedbackController(null);
            var result = controller.ChangeFeedbackStatus(-1);

            //Assert
            Assert.IsInstanceOfType(result as NotFoundResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ChangeFeedbackStatus_FeedbackIsNull_ShouldReturnNotFound()
        {
            var dbContext = new MockDbContext().GetContext();
            dbContext.Feedbacks.Add(new Feedback() { Id = 1 });
            dbContext.SaveChanges();

            //Act
            var controller = new FeedbackController(dbContext);
            var result = controller.ChangeFeedbackStatus(2);

            //Assert
            Assert.IsInstanceOfType(result as NotFoundResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ChangeFeedbackStatus_FeedbackIsRead_ShouldReturnIsNotRead()
        {
            var dbContext = new MockDbContext().GetContext();
            dbContext.Feedbacks.Add(new Feedback() { Id = 1, IsRead = true });
            dbContext.SaveChanges();

            //Act
            var controller = new FeedbackController(dbContext);
            var result = controller.ChangeFeedbackStatus(1);
            var feed = dbContext.Feedbacks.Find(1);
            //Assert
            Assert.AreEqual(feed.IsRead, false);
        }

        [TestMethod]
        public void ChangeFeedbackStatus_FeedbackIsNotRead_ShouldReturnIsRead()
        {
            var dbContext = new MockDbContext().GetContext();
            dbContext.Feedbacks.Add(new Feedback() { Id = 1, IsRead = false });
            dbContext.SaveChanges();

            //Act
            var controller = new FeedbackController(dbContext);
            var result = controller.ChangeFeedbackStatus(1);
            var feed = dbContext.Feedbacks.Find(1);
            //Assert
            Assert.AreEqual(feed.IsRead, true);
        }

        [TestMethod]
        public void ChangeFeedbackStatus_ShouldReturn_RedirectToActionResult()
        {
            var dbContext = new MockDbContext().GetContext();
            dbContext.Feedbacks.Add(new Feedback() { Id = 1, IsRead = false });
            dbContext.SaveChanges();

            //Act
            var controller = new FeedbackController(dbContext);
            var result = controller.ChangeFeedbackStatus(1);
            var redirectToAction = result as RedirectToActionResult;

            //Assert
            Assert.IsInstanceOfType(redirectToAction, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void ChangeFeedbackStatus_ShouldRedirectTo_AdminFeedbackAll()
        {
            var dbContext = new MockDbContext().GetContext();
            dbContext.Feedbacks.Add(new Feedback() { Id = 1, IsRead = false });
            dbContext.SaveChanges();

            //Act
            var controller = new FeedbackController(dbContext);
            var result = controller.ChangeFeedbackStatus(1);
            var redirectToAction = result as RedirectToActionResult;

            //Assert
            Assert.AreEqual(redirectToAction.RouteValues["area"], "admin");
            Assert.AreEqual(redirectToAction.ControllerName, "feedback");
            Assert.AreEqual(redirectToAction.ActionName, "all");
        }
    }
}
