using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Common.ExtensionModels;

namespace MyBlog.Common.Utilities
{
    public interface INotificationSender
    {
        bool SendNotification(string message, MessageType type, Controller controller = null, PageModel pageModel = null);
    }
}