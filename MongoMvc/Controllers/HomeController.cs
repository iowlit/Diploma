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
    public class HomeController : Controller
    {
        private readonly IDisciplineRepository _DisciplineRepository;
        private readonly ILecturerRepository _LecturerRepository;
        private readonly IStorageService _StorageService;

        public HomeController(IDisciplineRepository DisciplineRepository, ILecturerRepository LecturerRepository, IHostingEnvironment AppEnvironment)
        {
            _StorageService = new LocalFileStorageService(AppEnvironment);
            _DisciplineRepository = DisciplineRepository;
            _LecturerRepository = LecturerRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Read(int? page, string searchWord = null)
        {
            var dummyItems = await _DisciplineRepository.GetAllAsync();
            var pager = new Pager(dummyItems.Count(), page, 8);

            var viewModel = new DisciplinePagination
            {
                Disciplines = dummyItems.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager
            };
            ViewData["Title"] = "Усі дисципліни";
            return View(viewModel);            
        }

        [AllowAnonymous, HttpPost, ActionName("Read")]
        public async Task<IActionResult> SearchDiscipline(string searchWord)
        {
            ViewData["Title"] = "Результат пошуку: " + searchWord;
            ViewData["SearchWord"] = searchWord;
            var emptyModel = new DisciplinePagination { Disciplines = new List<Discipline>(), Pager = new Pager(0, 0) };
            if (String.IsNullOrEmpty(searchWord))
            {
                return View(emptyModel);
            }
            var dummyItems = await _DisciplineRepository.GetBySearchTextAsync(searchWord);

            if (dummyItems == null)
            {
                return View(emptyModel);
            }
            var pager = new Pager(dummyItems.Count(), 1, dummyItems.Count());            
            var viewModel = new DisciplinePagination
            {
                Disciplines = dummyItems,
                Pager = pager
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Lectures = new MultiSelectList(_LecturerRepository.GetAll(), "Id", "Name");
            ViewBag.Instructions = _StorageService.GetAllFiles("instructions").Select(w => w.Name).ToList();
            ViewBag.WorkSchedules = _StorageService.GetAllFiles("workschedule").Select(w => w.Name).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Discipline disc, string[] Ids)
        {
            disc.Lectors = _LecturerRepository.GetLectorsByArray(Ids);                                          
            await _DisciplineRepository.AddAsync(disc);
            ViewBag.Lectures = null;
            return RedirectToAction("Read");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
                return new BadRequestResult();
            }
            Discipline disc = await _DisciplineRepository.GetByIdAsync(id);
            if(disc == null)
            {
                return new NotFoundResult();
            }
            ViewBag.Lectures = new MultiSelectList(_LecturerRepository.GetAll(), "Id", "Name", disc.Lectors.Select(l => l.Id).ToList());
            ViewBag.Instructions = _StorageService.GetAllFiles("instructions").Select(w => w.Name).ToList();
            ViewBag.WorkSchedules = _StorageService.GetAllFiles("workschedule").Select(w => w.Name).ToList();
            return View(disc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Discipline disc, string[] Ids)
        {
            disc.Lectors = _LecturerRepository.GetLectorsByArray(Ids);
            await _DisciplineRepository.UpdateAsync(disc.Id, disc);
            ViewBag.Lectures = null;
            return RedirectToAction("Read");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            Discipline disc = await _DisciplineRepository.GetByIdAsync(id);
            if (disc == null)
            {
                return new NotFoundResult();
            }            
            return View(disc);
        }

        [HttpPost, ActionName("Delete")]       
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            await _DisciplineRepository.RemoveByIdAsync(id);           
            return RedirectToAction("Read");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Course(int id)
        {
            var dcs = await _DisciplineRepository.GetByCourseAsync(id);
            if (dcs == null)
            {
                return new NotFoundResult();
            }
            return View(dcs);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Discipline(string id)
        {
            var dcs = await _DisciplineRepository.GetByIdAsync(id);
            if (dcs == null)
            {
                return new NotFoundResult();
            }
            
            return View(dcs);
        }       
    }
}
