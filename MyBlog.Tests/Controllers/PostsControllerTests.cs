using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Controllers;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MyBlog.Tests.Controllers
{
    [TestClass]
    public class PostsControllerTests
    {
        private PostsController controller;
        private Post post;

        [TestMethod]
        public void Details_WithZero_ReturnsNotFound()
        {
            var result = controller.Details(0);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_WithNegative_ReturnsNotFound()
        {
            var result = controller.Details(-1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Details_WithInvalidId_ReturnsNotFound()
        {
            var dbContext = new MockDbContext().GetContext();

            controller = new PostsController(dbContext);

            var result = controller.Details(1);

            Assert.IsInstanceOfType(result as NotFoundResult, typeof(NotFoundResult));
        }

        [TestInitialize]
        public void InitializeController()
        {
            controller = new PostsController(null);

        }
    }
}
