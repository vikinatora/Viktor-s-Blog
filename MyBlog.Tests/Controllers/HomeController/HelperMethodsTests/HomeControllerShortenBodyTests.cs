using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBlog.Models;
using MyBlog.Controllers;

namespace MyBlog.Tests.Controllers.HomeController.HelperMethodsTests
{
    [TestClass]
    public class HomeControllerShortenBodyTests
    {
        private MyBlog.Controllers.HomeController controller;
        private const string DefaultTextForTests = "Hello, here is some text without a meaning. This text should show, how a printed text will look like at this place. If you read this text, you will get no information. Really? Is there no information? Is there a difference between this text and some nonsense like Huardest gefburn? Kjift – Never mind! A blind text like this gives you information about the selected font, how the letters are written and the impression of the look. This text should contain all letters of the alphabet and it should be written in of the original language.";

        private Post post;
        private readonly string TextThatIsLessThanMaxLength = DefaultTextForTests.Substring(0,30);
        private readonly string TextThatIsMaxLength =DefaultTextForTests.Substring(0, Common.Constants.MaximalHomePagePostBodyLength);
        private readonly string TextThatIsMoreThanMaxLength = DefaultTextForTests.Substring(0, Common.Constants.MaximalHomePagePostBodyLength +20);



        [TestMethod]
        public void ShortenBody_WtihBodyLessThanMaxLength_DoesntShortenBody()
        {
            post.Body = TextThatIsLessThanMaxLength;

            var result = controller.ShortenBody(post);

            Assert.AreEqual(TextThatIsLessThanMaxLength, post.Body);
        }

        [TestMethod]
        public void ShortenBody_WtihBodyEqualToMaxLength_DoesntShortenBody()
        {
            post.Body = TextThatIsMaxLength;

            var result = controller.ShortenBody(post);

            Assert.AreEqual(TextThatIsMaxLength, post.Body);
        }

        [TestMethod]
        public void ShortenBody_WtihBodyMoreThanMaxLength_ShortensBody()
        {
            post.Body = TextThatIsMaxLength;

            var result = controller.ShortenBody(post);

            Assert.AreEqual(result.Length, Common.Constants.MaximalHomePagePostBodyLength);
        }

        [TestInitialize]
        public void InitializeModelAndController()
        {
            controller = new MyBlog.Controllers.HomeController(null);
            post = new Post();

        }
    }
}
