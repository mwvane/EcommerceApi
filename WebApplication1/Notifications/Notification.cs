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
        public static  Notification SuccessfullyCreate<T>() where T : class
        {
            return new Notification()
            {
                Message = $"{typeof(T).Name} successfully created",
                Title = NotificationStatus.Success.ToString(),
                Status = NotificationStatus.Success,
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
