using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Common.ViewModels;
using MyBlog.Controllers;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTests
    {
        private SearchController controller;

        [TestMethod]
        public void Search_WithNotNullOrEmptyString_ReturnsProperView()
        {
            //Arrange

            InitializeDatabaseAndController();

            //Act
            var result = controller.Search("ha");

            //Assert

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Search_WithEmptyString_ReturnsNotFound()
        {
            //Arrange

            InitializeDatabaseAndController();

            //Act
            var result = controller.Search("");

            //Assert

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Search_WithNotNullOrEmptyString_CallsViewWithProperModel()
        {
            //Arrange
            InitializeDatabaseAndController();
            //Act
            var result = controller.Search("ha");
            var resultView = result as ViewResult;
            //Assert
            Assert.IsInstanceOfType(resultView.Model, typeof(SearchViewModel));
        }

        [TestMethod]
        public void Search_WithNull_ReturnsNotFound()
        {
            //Arrange

            InitializeDatabaseAndController();

            //Act
            var result = controller.Search(null);

            //Assert

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void FindObjectWithRightName_WithUppercaseParam_FindsLowercaseString()
        {
            //Arrange

            InitializeDatabaseAndController();

            //Act
            var result = controller.FindObjectWithRightName("ha", "HA");
            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FindObjectWithRightName_WithLowercaseParam_FindsUppercaseString()
        {
            //Arrange

            InitializeDatabaseAndController();

            //Act
            var result = controller.FindObjectWithRightName("HA", "ha");
            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FindObjectWithRightName_WithMixedCaseParam_FindsMixedcaseString()
        {
            //Arrange

            InitializeDatabaseAndController();

            //Act
            var result = controller.FindObjectWithRightName("VoLLeYBaLL", "vOllEyBall");
            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FindObjectWithRightName_WithDifferentParamAndTitle_ReturnsFalse()
        {
            //Arrange

            InitializeDatabaseAndController();

            //Act
            var result = controller.FindObjectWithRightName("volleyball", "football");
            //Assert

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HtmlizeTitle_WithIdenticalSearchTermAndTitle_TransformsWholeTitle()
        {
            //Arrange
            InitializeDatabaseAndController();
            var title = "volleyball";
            var searchTerm = "volleyball";
            var expectedString = $"<strong class=\"text-danger\">{title}</strong>";

            //Act
            var result = controller.HtmlizeTitle(searchTerm, title);
            //Assert

            Assert.AreEqual(expectedString, result);
        }

        [TestMethod]
        public void HtmlizeTitle_WithSearchTermAndPartTitle_TransformsPartiallyTitle()
        {
            //Arrange
            InitializeDatabaseAndController();
            var title = "volleyball";
            var searchTerm = "volley";

            var expectedString = $"<strong class=\"text-danger\">{searchTerm}</strong>ball";

            //Act
            var result = controller.HtmlizeTitle(searchTerm, title);
            //Assert

            Assert.AreEqual(expectedString, result);
        }

        [TestInitialize]
        public void InitializeDatabaseAndController()
        {
            var dbContext = new MockDbContext().GetContext();
            controller = new SearchController(dbContext);

        }
    }
}
