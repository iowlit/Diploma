using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MongoMvc.Data
{
    public interface IStorageService
    {
        Task<string> UploadAsync(string path, IFormFile content);
        List<IFileInfo> GetAllFiles(string path);
        void DeleteFile(string path);
    }

    public class LocalFileStorageService : IStorageService
    {
        private readonly IHostingEnvironment _env;
        private readonly IFileProvider _fileProvider;

        public LocalFileStorageService(IHostingEnvironment env)
        {
            _env = env;
            _fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        }

        public void DeleteFile(string path)
        {
            var fullPath = Path.Combine(_env.WebRootPath, "uploads", path).ToLower();
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);               
            }
        }

        public List<IFileInfo> GetAllFiles(string path)
        {
            IDirectoryContents contents = _fileProvider.GetDirectoryContents(Path.Combine("wwwroot", "uploads", path));

            var lastModified =
                      contents
                      .OrderByDescending(f => f.LastModified)
                      .ToList();

            return lastModified;
        }                

        public async Task<string> UploadAsync(string path, IFormFile content)
        {
            if (content != null && content.Length > 0)
            {
                string id = GetUniqueFileName(content.FileName);
                string extension = Path.GetExtension(content.FileName);
                
                string fileName = $"{ id ?? Guid.NewGuid().ToString() }{ extension }";
                
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

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString();
        }
    }
}
