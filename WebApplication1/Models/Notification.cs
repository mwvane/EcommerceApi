namespace EcommerceApp.Models
{
    public class Notification
    {
        public string? Id { get; set; } = Helpers.GenerateUniqueId(6);
        public string Title { get; set; } = "";
        public bool? AutoHide { get; set; } = true;
        public int? Duration { get; set; } = 5; //second
        public string Message { get; set; } = "";
        public NotificationStatus Status { get; set; } = NotificationStatus.Info;

    }
    public enum NotificationStatus
    {
        Success,
        Error,
        Warning,
        Info
    }

}
