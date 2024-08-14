using EcommerceApp;

namespace Ecommerce.Api.Notifications
{
    public interface INotification
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationStatus Status { get; set; }
    }
}
