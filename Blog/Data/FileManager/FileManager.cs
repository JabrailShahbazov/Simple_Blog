using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Blog.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _imagePath;

        public FileManager(IConfiguration configuration)
        {
            _imagePath = configuration["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
           return new FileStream(Path.Combine(_imagePath , image),FileMode.Open , FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var savePath = Path.Combine(_imagePath);
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{Guid.NewGuid()}{mime}";

                using (var fileStream =new FileStream(Path.Combine(savePath,fileName),FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
    }
}
