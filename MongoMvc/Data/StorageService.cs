using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MongoMvc.Data
{
    public interface IStorageService
    {
        Task<string> UploadAsync(string path, IFormFile content, string nameWithoutExtension = null);
    }

    public class LocalFileStorageService : IStorageService
    {
        private readonly IHostingEnvironment _env;

        public LocalFileStorageService(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(string path, IFormFile content, string nameWithoutExtension = null)
        {
            if (content != null && content.Length > 0)
            {
                string extension = Path.GetExtension(content.FileName);
                
                string fileName = $"{ nameWithoutExtension ?? Guid.NewGuid().ToString() }{ extension }";
                
                path = Path.Combine(_env.WebRootPath, "uploads", path).ToLower();
                
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                string fullFileLocation = Path.Combine(path, fileName).ToLower();

                // If your case, you might just need to open your 
                // `keys.json` and append text on it.
                // Note that there is FileMode.Append too you might want to
                // take a look.
                using (var fileStream = new FileStream(fullFileLocation, FileMode.Create))
                {
                    await content.CopyToAsync(fileStream);
                }

                // I only want to get its relative path
                return fullFileLocation.Replace(_env.WebRootPath,
                    String.Empty);
            }

            return String.Empty;
        }
    }
}
