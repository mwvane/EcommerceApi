using EcommerceApp;
using EcommerceApp.Controllers;

namespace Ecommerce.Api.Notifications
{
    public class Notification : INotification
    {
        public string? Id { get; set; } = Helpers.GenerateUniqueId(6);
        public string Title { get; set; } 
        public bool? AutoHide { get; set; } = true;
        public int? Duration { get; set; } = 5; //second
        public string Message { get; set; } = "";
        public NotificationStatus Status { get; set; } = NotificationStatus.Info;
        
    }
    public static class DefaultNotifications
    {
        public static  Notification Success<T>(CRUD_Action crudAction) where T : class
        {
            return new Notification()
            {
                Message = $"{typeof(T).Name} successfully {crudAction.ToString()}d",
                Title = NotificationStatus.Success.ToString(),
                Status = NotificationStatus.Success,
            };
        }

        public static Notification Error<T>(CRUD_Action crudAction, string message = "") where T : class
        {
            return new Notification()
            {
                Message = message == "" ? $"{typeof(T).Name} couldn't {crudAction.ToString()}d, try again" : message,
                Title = NotificationStatus.Error.ToString(),
                Status = NotificationStatus.Error,
            };
        }
    }
    public enum NotificationStatus
    {
        Success,
        Error,
        Warning,
        Info
    }
}
