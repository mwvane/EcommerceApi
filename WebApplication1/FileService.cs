﻿using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EcommerceApp
{
    public static  class FileService
    {

        public static async Task<IList<string>?> SaveFile(UploadFile file)
        {
            string path = "";
            switch (file.UploadType)
            {
                case UploadType.ProductImage:
                    path = "Images\\ProductImages";
                    break;
                case UploadType.Icon:
                    path = "Icons";
                    break;
                default:
                    path = "";
                    break;
            }
            try
            {
                var foldername = Path.Combine("Resources", path);
                var x = Directory.GetCurrentDirectory();
                if (!Directory.Exists(foldername))
                {
                    Directory.CreateDirectory(foldername);
                }
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), foldername);
                var urls = new List<string>();
                if (file.File.Count > 0)
                {
                    foreach (var item in file.File)
                    {
                        if (item.Length > 0)
                        {
                            // Generate a unique filename
                            var filePath = Path.Combine(foldername, DateTime.Now.ToBinary() + Path.GetFileName(item.FileName));

                            // Save the file
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                            }
                            urls.Add(filePath.Replace("\\","/"));
                        }

                        //var fileName = DateTime.Now.ToBinary() + ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim().ToString();
                        //var fullPath = Path.Combine(pathToSave, fileName);
                        //var dbPath = Path.Combine(foldername, fileName);
                        //using (var stream = new FileStream(fullPath, FileMode.Create))
                        //{
                        //    item.CopyTo(stream);
                        //    url = fullPath;
                        //}
                    }
                    return urls;
                   
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
                return null;
            }
            catch
            {
                return null;
            }
        }
    }

    public enum UploadType
    {
        None,
        ProductImage,
        UserImage,
        Icon,
        Documnet
    }
}
