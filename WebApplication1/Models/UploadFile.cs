using EcommerceApp;

namespace Ecommerce.Api.Models
{
    public class UploadFile
    {
        public int Id { get; set; }
        public IFormFileCollection File { get; set; }
        public UploadType UploadType { get; set; }
    }
}
