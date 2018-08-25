using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBlog.Controllers;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Tests.Mocks;
using System;

namespace MyBlog.Tests.Controllers.HomeController
{
    [TestClass]
    public class HomeControllerIndexTests
    {
        private MyBlog.Controllers.HomeController controller;

        [TestMethod]
        public void Index_WithCategoryFilterNullAndIdZero_ReturnsView()
        {
            InitializeController();
            var result = controller.Index(0);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void Index_WithCategoryFilterNullAndIdPostive_ReturnsView()
        {
            InitializeController();
            var result = controller.Index(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void Index_WithCategoryFilterNullAndIdNegative_ReturnsNotFound()
        {
            InitializeController();
            var result = controller.Index(-1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Index_WithCategoryFilterNotNullAndIdZero_ReturnsView()
        {
            InitializeController();
            var result = controller.Index(0, "ha");
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void Index_WithCategoryFilterNotNullAndIdPostive_ReturnsView()
        {
            InitializeController();
            var result = controller.Index(1, "ha");

            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void Index_WithCategoryFilterNotNullAndIdNegative_ReturnsNotFound()
        {
            InitializeController();
            var result = controller.Index(-1, "ha");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }



        [TestInitialize]
        public void InitializeController()
        {
            var dbContext = new MockDbContext().GetContext();
            controller = new MyBlog.Controllers.HomeController(dbContext);
        }
    }
}
