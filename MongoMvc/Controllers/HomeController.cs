using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoMvc.Interfaces;
using MongoMvc.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MongoMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Discipline> _DisciplineRepository;
        private readonly ILecturerRepository _LecturerRepository;

        public HomeController(IRepository<Discipline> DisciplineRepository, ILecturerRepository LecturerRepository)
        {
            _DisciplineRepository = DisciplineRepository;
            _LecturerRepository = LecturerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Read()
        {
            //IEnumerable<Note> noteElements = await _noteRepository.GetAllNotes();
            //ViewData["Message"] = string.Format($"Note Id: {Id} - Body: {noteElement.Body}");

            return View(await _DisciplineRepository.GetAllAsync());
        }

        public IActionResult Init()
        {
            _LecturerRepository.Add(new Lecturer() { Name = "Гнатів Богдан Васильович", Descr = "доцент, кандидат фіз-мат наук" });
            _LecturerRepository.Add(new Lecturer() { Name = "Гладун Володимир Романович", Descr = "доцент, кандидат фіз-мат наук" });
            _DisciplineRepository.Add(new Discipline() { Name = "Математичний аналіз, ч.1", ModuleType = ModuleType.Required, ModuleDescr = "загальна кількість годин - 240 (Кредитів: EKTS - 8)..", Lectors = _LecturerRepository.GetAll().ToList(), YearPart = YearPart.Autumn, Course = 1, ControlType = ControlType.Exam, UpdatedOn = DateTime.Now });
            _DisciplineRepository.Add(new Discipline() { Name = "Математичний аналіз, ч.1", ModuleType = ModuleType.Required, ModuleDescr = "загальна кількість годин - 240 (Кредитів: EKTS - 8)..", Lectors = _LecturerRepository.GetAll().ToList(), YearPart = YearPart.Autumn, Course = 1, ControlType = ControlType.Exam, UpdatedOn = DateTime.Now });


            ViewData["Message"] = string.Format($"Filled in 4 records");
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Lectures = new MultiSelectList(_LecturerRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Discipline disc, string[] Id)
        {
            disc.Lectors = _LecturerRepository.GetLectorsByArray(Id); ;
            _DisciplineRepository.AddAsync(disc);
            return RedirectToAction("Read");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
