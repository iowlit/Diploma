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

        [HttpGet]
        public async Task<IActionResult> CreateWorkSchedule()
        {
            return View();
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

        [HttpGet]
        public async Task<IActionResult> CreateInstruction()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstruction(List<IFormFile> files)
        {
            if (files.Count != 0)
            {
                foreach (IFormFile file in files)
                {
                    await _StorageService.UploadAsync("instructions", file);
                }
            }
            return RedirectToAction("Instructions");
        }

        [HttpGet]
        public IActionResult DeleteInstruction(String fileName)
        {
            var file = _StorageService.GetAllFiles("instructions").Where(f => f.Name == fileName).FirstOrDefault();
            return View(file);
        }

        [HttpPost, ActionName("DeleteInstruction")]
        public IActionResult ConfirmDeleteInstruction(String fileName)
        {
            _StorageService.DeleteFile(Path.Combine("instructions", fileName));
            return RedirectToAction("Instructions");
        }

        [HttpGet]
        public IActionResult DeleteWorkSchedule(String fileName)
        {
            var file = _StorageService.GetAllFiles("workschedule").Where(f => f.Name == fileName).FirstOrDefault();
            return View(file);
        }

        [HttpPost, ActionName("DeleteWorkSchedule")]
        public IActionResult ConfirmDeleteWorkSchedule(String fileName)
        {
            _StorageService.DeleteFile(Path.Combine("workschedule", fileName));
            return RedirectToAction("WorkSchedules");
        }
    }
}
