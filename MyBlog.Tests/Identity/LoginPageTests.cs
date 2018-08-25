//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using MyBlog.Areas.Identity.Pages.Account;
//using MyBlog.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

//namespace MyBlog.Tests.Identity
//{
//    [TestClass]
//    public class LoginPageTests
//    {
//        [TestMethod]
//        public void Login_WithNormalUser_RedirectsToHomeIndex()
//        {
//            //Arrange
//            var mockUserStore = new Mock<IUserStore<User>>();

//            var mockUserManager = new Mock<UserManager<User>>(
//                mockUserStore.Object, null, null, null, null, null, null, null, null);

//            mockUserManager.Setup(um => um.GetRolesAsync(null))
//                .ReturnsAsync(new List<string>());

//            var mockSigInManager = new Mock<SignInManager<User>>(
//                mockUserManager.Object,
//                new HttpContextAccessor(),
//                  new Mock<IUserClaimsPrincipalFactory<User>>().Object,
//                  new Mock<IOptions<IdentityOptions>>().Object,
//                  new Mock<ILogger<SignInManager<User>>>().Object,
//                  new Mock<IAuthenticationSchemeProvider>().Object
//                );


//            //mockSigInManager.Setup(
//            //       x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
//            //   .ReturnsAsync(SignInResult.Success);

//            //mockSigInManager.Setup(
//            //       x => x.PasswordSignInAsync(new User(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
//            //   .ReturnsAsync(SignInResult.Success);

//            var controller = new LoginModel(mockSigInManager.Object, null, mockUserManager.Object);

//            var result = controller.OnPostAsync("/home/index").Result;

//            var redirectToActionResult = result as RedirectToActionResult;
//        }
//    }
//}
