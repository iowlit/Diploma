using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoMvc.Interfaces;
using MongoMvc.Model;
using System.Collections.Generic;
using System.Linq;

namespace MongoMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Discipline> _DisciplineRepository;
        private readonly IRepository<Lecturer> _LecturerRepository;

        public HomeController(IRepository<Discipline> DisciplineRepository, IRepository<Lecturer> LecturerRepository)
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

            return View(await _DisciplineRepository.GetAllNotesAsync());
        }

        public IActionResult Init()
        {
            _DisciplineRepository.RemoveAllNotes();
            _LecturerRepository.RemoveAllNotes();
            _LecturerRepository.AddNote(new Lecturer() { Name = "Гнатів Богдан Васильович", Descr = "доцент, кандидат фіз-мат наук" });
            _LecturerRepository.AddNote(new Lecturer() { Name = "Гладун Володимир Романович", Descr = "доцент, кандидат фіз-мат наук" });
            _DisciplineRepository.AddNote(new Discipline() { Name = "Математичний аналіз, ч.1", ModuleType = ModuleType.Required, ModuleDescr = "загальна кількість годин - 240 (Кредитів: EKTS - 8)..", Lectors = _LecturerRepository.GetAllNotes().ToList(), YearPart = YearPart.Autumn, Course = 1, ControlType = ControlType.Exam, CreatedOn = DateTime.Now });
            _DisciplineRepository.AddNote(new Discipline() { Name = "Математичний аналіз, ч.1", ModuleType = ModuleType.Required, ModuleDescr = "загальна кількість годин - 240 (Кредитів: EKTS - 8)..", Lectors = _LecturerRepository.GetAllNotes().ToList(), YearPart = YearPart.Autumn, Course = 1, ControlType = ControlType.Exam, CreatedOn = DateTime.Now });


            ViewData["Message"] = string.Format($"Filled in 4 records");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
