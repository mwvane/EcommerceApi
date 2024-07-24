using Microsoft.AspNetCore.Http.HttpResults;

namespace EcommerceApp.Models
{
    public class Result
    {
        public object? Data { get; set; }
        public string?  Success { get; set; }
        public Notification? Notification { get; set; }
        public List<string>? Error { get; set; }

    }
}
