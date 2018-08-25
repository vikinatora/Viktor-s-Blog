using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Common.ExtensionModels;
using MyBlog.Common.Utilities;
using MyBlog.Controllers;
using MyBlog.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Tests.Utilities
{
    [TestClass]
    public class NotificationSenderTests
    {
        //Can't write unit tests as Moq can't test extensions methods :/

        [TestMethod]
        public void Sender_WithNoControllerAndNoPageModel_ShouldReturnFalse()
        {
            var sender = new NotificationSender();

            var result = sender.SendNotification("test", MessageType.Success, null, null);

            Assert.AreEqual(false, result);
        }

    }
}
