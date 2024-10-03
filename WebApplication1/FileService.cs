using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EcommerceApp
{
    public static  class FileService
    {
        public static Dictionary<string,string> directories = new Dictionary<string, string>()
        {
            {"countries", "Countries" },
            {"icons", "Icons" },
            {"others", "Others" },
            {"productimages", "ProductImages" },
        };
        public static async Task<string> SaveImageToStorage(IFormFile image, UploadType type = UploadType.Others)
        {
            var folderPath = Path.Combine("Resources", directories[type.ToString().ToLower()]);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var fullPath = Path.Combine(folderPath, fileName).Replace("\\", "/");
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return fullPath;
        }

        public static async Task<List<string>> SaveImageListToString(IFormFileCollection images, UploadType type = UploadType.Others)
        {
            var imagesList = new List<string>();

            foreach (var image in images)
            {
                if (image != null)
                {
                    var imageString = await SaveImageToStorage(image, type);
                    imagesList.Add(imageString);
                }
            }

            return imagesList;
        }

        public static string GetFullPath(string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path).Replace("\\", "/");
            return fullPath;
        }

        //public static async Task<IList<string>?> SaveFile(UploadFile file)
        //{
        //    string path = "";
        //    switch (file.UploadType)
        //    {
        //        case UploadType.ProductImage:
        //            path = "Images\\ProductImages";
        //            break;
        //        case UploadType.Icon:
        //            path = "Icons";
        //            break;
        //        default:
        //            path = "";
        //            break;
        //    }
        //    try
        //    {
        //        var foldername = Path.Combine("Resources", path);
        //        var x = Directory.GetCurrentDirectory();
        //        if (!Directory.Exists(foldername))
        //        {
        //            Directory.CreateDirectory(foldername);
        //        }
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), foldername);
        //        var urls = new List<string>();
        //        if (file.File.Count > 0)
        //        {
        //            foreach (var item in file.File)
        //            {
        //                if (item.Length > 0)
        //                {
        //                    // Generate a unique filename
        //                    var filePath = Path.Combine(foldername, DateTime.Now.ToBinary() + Path.GetFileName(item.FileName));

        //                    // Save the file
        //                    using (var stream = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        await item.CopyToAsync(stream);
        //                    }
        //                    urls.Add(filePath.Replace("\\","/"));
        //                }

        //                //var fileName = DateTime.Now.ToBinary() + ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim().ToString();
        //                //var fullPath = Path.Combine(pathToSave, fileName);
        //                //var dbPath = Path.Combine(foldername, fileName);
        //                //using (var stream = new FileStream(fullPath, FileMode.Create))
        //                //{
        //                //    item.CopyTo(stream);
        //                //    url = fullPath;
        //                //}
        //            }
        //            return urls;

        //            //var user = _context.Users.FirstOrDefault(item => item.Id == file.UserId);
        //            //var category = _context.Categories.FirstOrDefault(c => c.Id == file.UserId);
        //            //if (category != null)
        //            //{
        //            //    category.ImageUrl = dbPath;
        //            //    _context.Categories.Update(category);
        //            //    _context.SaveChanges();
        //            //}
        //            //if (user != null)
        //            //{
        //            //    if (path == Constants.IMAGES_FOLDER_NAME)
        //            //    {
        //            //        user.ImageUrl = dbPath;
        //            //    }
        //            //    else
        //            //    {
        //            //        user.CV = dbPath;
        //            //    }
        //            //    _context.Users.Update(user);
        //            //    _context.SaveChanges();
        //            //}

        //        }
        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }

    public enum UploadType
    {
        None,
        ProductImages,
        UserImage,
        Icon,
        Documnet,
        Countries,
        Others
    }
}
