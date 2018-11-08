using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoMvc.Model;
using MongoMvc.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoMvc.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MongoMvc.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {

        private readonly IStorageService _StorageService;
                
        public FilesController(IHostingEnvironment AppEnvironment)
        {
            _StorageService = new LocalFileStorageService(AppEnvironment);
        }

        // GET: /<controller>/
        public IActionResult WorkSchedules()
        {
            return View(_StorageService.GetAllFiles("workschedule"));
        }

        public IActionResult Instructions()
        {
            return View(_StorageService.GetAllFiles("instructions"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkSchedule(List<IFormFile> files)
        {
            if (files.Count != 0)
            {
                foreach (IFormFile file in files)
                {                    
                    await _StorageService.UploadAsync("workschedule", file);
                }
            }
            return RedirectToAction("WorkSchedules");
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstruction(List<IFormFile> files)
        {
            if (files.Count != 0)
            {
                foreach (IFormFile file in files)
                {
                    await _StorageService.UploadAsync("instruction", file);
                }
            }
            return RedirectToAction("Instructions");
        }

        [HttpPost]
        public IActionResult DeleteInstruction(IFormFile file)
        {
            return RedirectToAction("Instructions");
        }

        [HttpPost]
        public IActionResult DeleteWorkSchedule(IFormFile file)
        {
            return RedirectToAction("Instructions");
        }
    }
}
