using Ecommerce.Api.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Models
{
    public class Response
    {
        public object? Data { get; set; }
        public Notification? Notification { get; set; }
        public List<string>? Error { get; set; }
    }
}