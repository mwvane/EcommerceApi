using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace EcommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly Context _context;
        public FileController(Context context)
        {
            _context = context;
        }

        [HttpPost("UploadProductImage"), DisableRequestSizeLimit]
        public Result UploadProductImage([FromForm] UploadFile file)
        {
            string? url = SaveFile(file, Constants.ICONS_FOLDER_NAME);
            if (url != null)
            {
                return new Result() { Data = url };
            }
            return new Result() { Error = new List<string>() { "file not saved" } };
        }

        private string? SaveFile(UploadFile file, string path)
        {
            try
            {
                var foldername = Path.Combine("Resources", path);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), foldername);
                string? url = null;
                if (file.File.Length > 0)
                {
                    var fileName = DateTime.Now.ToBinary() + ContentDispositionHeaderValue.Parse(file.File.ContentDisposition).FileName.Trim().ToString();
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(foldername, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.File.CopyTo(stream);
                        url = fullPath;
                    }
                    //var user = _context.Users.FirstOrDefault(item => item.Id == file.UserId);
                    //var category = _context.Categories.FirstOrDefault(c => c.Id == file.UserId);
                    //if (category != null)
                    //{
                    //    category.ImageUrl = dbPath;
                    //    _context.Categories.Update(category);
                    //    _context.SaveChanges();
                    //}
                    //if (user != null)
                    //{
                    //    if (path == Constants.IMAGES_FOLDER_NAME)
                    //    {
                    //        user.ImageUrl = dbPath;
                    //    }
                    //    else
                    //    {
                    //        user.CV = dbPath;
                    //    }
                    //    _context.Users.Update(user);
                    //    _context.SaveChanges();
                    //}

                }
                return url;
            }
            catch
            {
                return null;
            }
            return null;
        }

    }
}
