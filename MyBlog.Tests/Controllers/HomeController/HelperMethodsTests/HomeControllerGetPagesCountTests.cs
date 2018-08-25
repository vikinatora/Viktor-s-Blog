using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBlog.Models;

namespace MyBlog.Tests.Controllers.HomeController.HelperMethodsTests
{
    [TestClass]
    public class HomeControllerGetPagesCountTests
    {
        private MyBlog.Controllers.HomeController controller;
        private Post post;

        [TestMethod]
        public void GetPages_WithZero_ReturnsOnePage()
        {
            var result = controller.GetPagesCount(0);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetPages_WithNegative_ReturnsOnePage()
        {
            var result = controller.GetPagesCount(-2);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void GetPages_WithUnevenPostsPerPage_ReturnsCorrectNumberOfPages()
        {
            var result = controller.GetPagesCount(4);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetPages_WithEvenPostsPerPage_ReturnsCorrectNumberOfPages()
        {
            var result = controller.GetPagesCount(6);
            Assert.AreEqual(2, result);
        }

        [TestInitialize]
        public void InitializeModelAndController()
        {
            controller = new MyBlog.Controllers.HomeController(null);
            post = new Post();

        }
    }
}
