using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Common.ExtensionModels;
using MyBlog.Helpers;
using System;

namespace MyBlog.Common.Utilities
{
    public class NotificationSender : INotificationSender
    {
        public  bool SendNotification(string message, MessageType type, Controller controller = null, PageModel pageModel = null)
        {
            var messageModel = new MessageModel()
            {
                Message = message,
                Type = type
            };

            if (controller == null && pageModel == null)
            {
                return false;
            }
            else if (controller == null)
            {
                pageModel.TempData.Put<MessageModel>("__Message", messageModel);
                return true;

            }
            else
            {
                controller.TempData.Put<MessageModel>("__Message", messageModel);
                return true;
            }

        }
    }
}